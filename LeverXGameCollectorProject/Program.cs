using FluentValidation;
using LeverXGameCollectorProject.Application;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Features.Developer.Validators;
using LeverXGameCollectorProject.Infrastructure;
using LeverXGameCollectorProject.Infrastructure.Persistence;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new DatabaseSettings
{
    ConnectionString = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")
});

var repositoryType = builder.Configuration.GetValue<string>("RepositorySettings:RepositoryType");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateDeveloperCommand>())
    .AddValidatorsFromAssembly(typeof(CreateDeveloperCommandValidator).Assembly);

//switch (repositoryType?.ToUpperInvariant())
//{
//    case "DAPPER":
//        builder.Services.AddInfrastructure("DAPPER");
//        break;

//    case "EFCORE":
//        builder.Services.AddInfrastructure("EFCORE");
//        builder.Services.AddDbContext<ApplicationDbContext>(options =>
//            options.UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"),
//            x => x.MigrationsAssembly("LeverXGameCollectorProject.Migrations")));
//        break;
//    case "INMEMORY":
//        builder.Services.AddInfrastructure();
//        break;
//    default:
//        var errorMessage = $"Invalid repository type: {repositoryType}. Valid options: Dapper, EFCore";
//        throw new InvalidOperationException(errorMessage);
//}


builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"),
            x => x.MigrationsAssembly("LeverXGameCollectorProject.Migrations")));

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameCollection API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 4;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandler?.Error;

        var (statusCode, message) = exception switch
        {
            PostgresException { SqlState: "23503" } =>
                (StatusCodes.Status400BadRequest, "The entry cannot be deleted, modified or inserted because it is in use, does not exist or constains invalid data."),
            DbUpdateException ex when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503") => 
                (StatusCodes.Status400BadRequest, "The entry cannot be deleted, modified or inserted because it is in use, does not exist or constains invalid data."),
            _ => (StatusCodes.Status500InternalServerError, "An internal server error occurred")
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new { error = message });
    });
});

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Application started with {repositoryType} repository", repositoryType);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();

using FluentValidation;
using LeverXGameCollectorProject.API;
using LeverXGameCollectorProject.Application;
using LeverXGameCollectorProject.Application.Behaviors;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Features.Developer.Validators;
using LeverXGameCollectorProject.Domain;
using LeverXGameCollectorProject.Domain.Entities.DB;
using LeverXGameCollectorProject.Infrastructure;
using LeverXGameCollectorProject.Infrastructure.Persistence;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

const string _errorBr = "The entry cannot be deleted, modified or inserted because it is in use, does not exist or constains invalid data.";
const string _errorIs = "An internal server error occurred";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new DatabaseSettings
{
    ConnectionString = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? jwtSettings["SecretKey"];

builder.Services.AddHealthChecks();

var repositoryType = builder.Configuration.GetValue<RepositoryType>("RepositorySettings:RepositoryType");

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<CreateDeveloperCommand>();
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    }
)
    .AddValidatorsFromAssembly(typeof(CreateDeveloperRequestModelValidator).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"),
    x => x.MigrationsAssembly("LeverXGameCollectorProject.Migrations")));

builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["ValidIssuer"],
        ValidAudience = jwtSettings["ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

builder.Services
    .AddApplication()
    .AddInfrastructure(RepositoryType.EFCore);

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
                (StatusCodes.Status400BadRequest, _errorBr),
            DbUpdateException ex when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503") => 
                (StatusCodes.Status400BadRequest, _errorBr),
            _ => (StatusCodes.Status500InternalServerError, _errorIs)
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new { error = message });
    });
});

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Application started with {repositoryType} repository", repositoryType);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    await IdentitySeeder.SeedRolesAndAdminAsync(services);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();

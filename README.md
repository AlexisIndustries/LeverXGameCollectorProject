# GameCollection API 🎮

GameCollection API is a modern RESTful service for managing video game collections, built on ASP.NET Core using Clean Architecture. The service allows tracking games, platforms, genres, developers, and user reviews.

## ✨ Features

- 🚀 Full-featured REST API for managing game collections
- 🔒 JWT authentication and authorization
- ⚡ Real-time search with autocomplete via WebSocket
- 🧩 Clean Architecture
- 📦 Docker and Docker Compose support
- 📊 Swagger UI for API testing
- 🧪 Unit and integration tests
- 🔄 CQRS with MediatR
- 🧠 Entity Framework Core and Dapper

## 🛠 Technologies

- ASP.NET Core 8
- PostgreSQL
- Entity Framework Core
- Docker
- JWT Authentication
- MediatR
- AutoMapper
- FluentValidation
- xUnit
- Swagger

## 🚀 Running the Project

### With Docker Compose

```bash
docker-compose up --build
```

The application will be available at: [http://localhost:8080](http://localhost:8080)  
Swagger UI: [http://localhost:8080/swagger](http://localhost:8080/swagger)

### Without Docker

1. Install PostgreSQL
2. Create database `GameCollectionDb`
3. Update connection string in `appsettings.json`
4. Run the project:

```bash
dotnet run --project LeverXGameCollectorProject.API
```

## 📚 Project Structure

```
GameCollection/
├── API/                  # Web layer (controllers, middleware)
├── Application/          # Business logic (services, DTO, CQRS)
├── Domain/               # Domain models and interfaces
├── Infrastructure/       # Repository implementations, Identity
├── Tests/                # Unit and integration tests
```

## 🔐 Authentication

To access protected endpoints:
1. Register a user: `POST /api/auth/register`
2. Login: `POST /api/auth/login`
3. Use the received JWT token in the header:
   ```
   Authorization: Bearer <your_token>
   ```

## ⚙️ Environment Configuration

Create a `.env` file in the project root:

```env
# PostgreSQL
POSTGRES_PASSWORD=your_strong_password

# JWT
JWT_SECRET_KEY=your_very_strong_secret_key_at_least_32_characters
JWT_ISSUER=GameCollectionAPI
JWT_AUDIENCE=GameCollectionClient
JWT_EXPIRY_MINUTES=60
```

## 🌐 API Endpoints

| Method | Path                  | Description                |
|-------|-----------------------|-------------------------|
| POST  | /api/auth/register    | Register user |
| POST  | /api/auth/login       | Login          |
| GET   | /api/games            | Get all games       |
| POST  | /api/games            | Create new game      |
| GET   | /api/games/{id}       | Get game by ID     |
| PUT   | /api/games/{id}       | Update game           |
| DELETE| /api/games/{id}       | Delete game            |

## 🧪 Testing

```bash
# Run unit tests
dotnet test

# Run integration tests
dotnet test --filter "Category=Integration"
```

**GameCollection API** © 2025. Built with ❤️ for gaming geeks.
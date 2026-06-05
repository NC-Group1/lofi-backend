# 🎧 Calico Backend

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/ORM-EF_Core-512BD4?style=for-the-badge&logo=efcore&logoColor=white)
![Database](https://img.shields.io/badge/Database-SQLite_/_SQL_Server-336791?style=for-the-badge&logo=sqlite&logoColor=white)
![Swagger](https://img.shields.io/badge/Docs-Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![HealthChecks](https://img.shields.io/badge/Monitoring-HealthChecks-FF6F00?style=for-the-badge&logo=heartbeat&logoColor=white)
![Testing](https://img.shields.io/badge/Testing-NUnit_/_Moq_/_Shouldly-512BD4?style=for-the-badge&logo=testinglibrary&logoColor=white)
![Design](https://img.shields.io/badge/Design-UML_/_Figma-000000?style=for-the-badge&logo=figma&logoColor=white)


Calico Backend is a production-style ASP.NET Core Web API built as part of a collaborative full-stack group project. It powers the Calico LoFi platform by providing robust media streaming, productivity tracking, and secure identity management through a highly maintainable, testable, and decoupled layered architecture.

---

# 🚀 Overview
The backend is designed for real‑world maintainability and scalability, featuring:

* Layered architecture (Controllers → Services → Repositories → EF Core → Database)

* Dual‑database strategy (SQLite for development, SQL Server for production)

* Automatic schema creation/migration

* Health checks for API and database

* Swagger/OpenAPI documentation

* Fully tested business logic, controllers, and data access

# 🧰 Tech Stack

  | Area | Technologies |
| --- | --- |
| **Framework** | ASP.NET Core Web API (.NET 8) |
| **Language** | C# |
| **Authentication** | Supabase JWT (cookie‑based token extraction) |
| **ORM** | Entity Framework Core |
| **Databases** | SQLite (Dev), SQL Server (Prod) |
| **Testing** | NUnit, Moq, Shouldly |
| **Tools** | Swagger, HealthChecks, HttpClient |


# 🎯 Core Features

## User Management
* User CRUD

* Authentication integration

* Profile operations

## Music & Media
* Music endpoints

* YouTube integration via repository/service layer

## Playlists & Projects
* Playlist CRUD

* Project CRUD

## Task Timer
* Timer CRUD

* Productivity tracking

## System Features
* Health checks (/health)

* Swagger API documentation

# 🛠️ Planned Features
* Role‑based access control (RBAC)

* Admin endpoints

* Caching layer (Redis or MemoryCache)
  
* Authentication flow

# 🏗️ Architecture
Calico Backend follows a clean, decoupled layered architecture:

Controllers
    ↓
Services (Business Logic)
    ↓
Repositories (Data Access)
    ↓
Entity Framework Core
    ↓
Database (SQLite / SQL Server)

# 🧪 Testing

The backend includes automated test coverage across all major layers.

## Backend Tests
* Repository testing

* Service testing

* Controller testing

## Testing Tools
* NUnit — test framework

* Moq — mocking dependencies

* Shouldly — readable assertions

* In‑memory SQLite — integration‑style repository tests

## Database Configuration
* Development: SQLite (auto‑created with EnsureCreated())

* Production: SQL Server (migrations applied via Migrate())

## Health Checks
* ApiHealthCheck

* DatabaseHealthCheck

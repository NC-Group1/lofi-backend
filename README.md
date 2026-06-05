# 🎧 Calico Backend

[![.NET Version](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![Auth](https://img.shields.io/badge/Auth-Supabase%20JWT-green.svg)](https://supabase.com/)
[![Database](https://img.shields.io/badge/Database-SQLite%20%7C%20SQL%20Server-blue.svg)]()
[![Design Blueprint](https://img.shields.io/badge/Design-UML%20%7C%20Figma-blue.svg)](#-design--planning)

Calico Backend is a production-style ASP.NET Core Web API built as part of a collaborative full-stack group project. It powers the Calico LoFi platform by providing robust media streaming, productivity tracking, and secure identity management through a highly maintainable, testable, and decoupled layered architecture.

---

# 🚀 Overview
The backend is designed for real‑world maintainability and scalability, featuring:

* Layered architecture (Controllers → Services → Repositories → EF Core → Database)

* Secure Supabase JWT authentication

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
## Authentication
* Supabase JWT validation

* Secure cookie‑based token extraction (supabase_jwt)

* Issuer, audience, and lifetime validation

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

* Automatic DB creation/migration

# 🛠️ Planned Features
* Role‑based access control (RBAC)

* Admin endpoints

* Caching layer (Redis or MemoryCache)

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

# 🔐 Authentication & Program Setup

The backend uses Supabase JWT authentication with secure cookie extraction.

## Database Configuration
* Development: SQLite (auto‑created with EnsureCreated())

* Production: SQL Server (migrations applied via Migrate())

## Health Checks
* ApiHealthCheck

* DatabaseHealthCheck

## 📁 Project Structure

lofi-backend-sol/
├── lofi-backend/               # Main Application
│   ├── Controllers/            # API Endpoints
│   ├── Data Models/            # Entities, ViewModels, Enums
│   ├── Database/               # EF Core DbContext
│   ├── HealthChecks/           # API + DB diagnostics
│   ├── Repository/             # Data access layer
│   └── Service/                # Business logic layer
└── Testing/                    # Automated Tests (NUnit)
    └── [Auth | Music | Playlist | Projects | TaskTimer | Users]


# 🏋️ AQWA — Backend

> ASP.NET Core 8 REST API powering the gym management system. Clean Architecture, PostgreSQL, and a domain that actually understands what a gym needs.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Domain Entities](#-domain-entities)
- [API Endpoints](#-api-endpoints)
- [Getting Started](#-getting-started)
- [Key Design Decisions](#-key-design-decisions)

---

## 🔍 Overview

This is the backend service for the Gym Management System. It exposes a RESTful API consumed by the React frontend and is responsible for all business logic: member lifecycle, subscription tracking, plan/offer management, body measurement history, and staff operations.

---

## 🛠️ Tech Stack

| Tool | Purpose |
|---|---|
| **ASP.NET Core 8** | Web API framework |
| **C#** | Language |
| **Entity Framework Core** | ORM / database access |
| **PostgreSQL** | Relational database (hosted on Supabase) |
| **Clean Architecture** | Structural pattern (Onion model) |

---

## 🏗️ Architecture

This project follows **Clean Architecture** — dependencies point inward only. The domain has zero knowledge of infrastructure or HTTP.

```
GymManagement.API           ← Entry point: controllers, middleware, DI wiring
     │
     ▼
GymManagement.Application   ← Use cases, CQRS handlers, DTOs, mapping
     │
     ▼
GymManagement.Infrastructure ← EF Core DbContext, migrations, Supabase/Postgres
     │
     ▼
GymManagement.Domain        ← Entities, interfaces, enums — pure business rules
```

No outer layer references an inner layer. The domain is king. 👑

---

## 📁 Project Structure

```
gym-management-backend/
├── src/
│   ├── GymManagement.API/
│   │   ├── Controllers/       # HTTP endpoints
│   │   ├── Middleware/        # Request pipeline (error handling, etc.)
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── GymManagement.Application/
│   │   ├── Features/          # Use cases (organized by feature)
│   │   ├── Common/            # Shared interfaces, base types
│   │   └── Mappings/          # DTO ↔ domain mapping
│   │
│   ├── GymManagement.Domain/
│   │   ├── Entities/          # Core business entities
│   │   ├── Interfaces/        # Repository contracts
│   │   └── Enums/
│   │
│   └── GymManagement.Infrastructure/
│       ├── Persistence/       # EF Core DbContext, repositories
│       ├── Migrations/
│       └── Services/
│
└── tests/
    ├── GymManagement.Domain.Tests/
    └── GymManagement.Application.Tests/
```

---

## 🧬 Domain Entities

| Entity | Description |
|---|---|
| `Gym` | Top-level tenant entity — all data is GymId-scoped |
| `Member` | Gym members (phone number is the unique business key) |
| `SubscriptionPlan` | Plan definitions (duration, price) |
| `Offer` | Promotional pricing tied to plans |
| `MemberSubscription` | Tracks active/expired member subscriptions |
| `Measurement` | Body measurement records per member |
| `Role` | Extensible role system for access control |
| `User` | System users (staff, admin) |

---

## 🌐 API Endpoints

| Controller | Base Route | Responsibility |
|---|---|---|
| `AuthController` | `/api/auth` | Login / token management |
| `MembersController` | `/api/members` | Member CRUD + lookup |
| `SubscriptionsController` | `/api/subscriptions` | Subscribe, renew, expire |
| `PlansController` | `/api/plans` | Manage subscription plans |
| `OffersController` | `/api/offers` | Manage promotional offers |
| `MeasurementsController` | `/api/measurements` | Track member body measurements |
| `StaffController` | `/api/staff` | Staff management |
| `RolesController` | `/api/roles` | Role management |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL database (local or [Supabase](https://supabase.com))

### Setup

```bash
# 1. Clone and navigate
cd gym-management-backend

# 2. Restore dependencies
dotnet restore

# 3. Configure connection string
# Edit src/GymManagement.API/appsettings.json:
# "ConnectionStrings": { "DefaultConnection": "your-postgres-connection-string" }

# 4. Apply migrations
dotnet ef database update --project src/GymManagement.Infrastructure --startup-project src/GymManagement.API

# 5. Run
dotnet run --project src/GymManagement.API
```

API will be available at `https://localhost:7xxx` (check console output for exact port).

### Running Tests

```bash
dotnet test
```

---

## 🧠 Key Design Decisions

| Decision | Why |
|---|---|
| **Phone = unique business key** | Members are identified by phone in real gym workflows; internal PK stays a Guid |
| **GymId on every tenant entity** | Single-gym today, multi-tenant ready tomorrow — EF global query filters enforce isolation |
| **Week = Sunday → Saturday** | Matches local calendar convention for expiry queries |
| **Roles are extensible** | Designed to grow: admin, trainer, receptionist, etc. — without schema changes |
| **No member-facing portal (yet)** | Admin/staff-only MVP first; member portal is the next phase |

---

*No shortcuts. No spaghetti. Just clean layers and domain logic that earns its keep. 💪*

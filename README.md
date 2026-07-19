# Track Management Backend

A .NET Web API for a music distribution company to manage artists, tracks, and their
distribution status across DSPs (Digital Service Providers) such as Spotify, Apple Music,
and Amazon Music.

## Overview

The API exposes endpoints to create artists and tracks, list/filter tracks, view a track's
full details (including its per-DSP distribution status), submit a track to one or more
DSPs, and update a track's status. The solution follows **Clean Architecture**, separating
domain rules, application logic, infrastructure concerns, and the API host into independent
projects.

## Features

- CRUD-style endpoints for artists and tracks
- Filtering tracks by artist, genre, and status
- Track-to-DSP distribution workflow (`Draft` → `Submitted` → `Distributed`)
- Per-track distribution status tracking (`Pending` / `Live` / `Rejected`)
- Request validation with FluentValidation and consistent error responses
- Centralized exception handling middleware (404 / 409 / 500 mapping)
- JWT Bearer authentication (protects the distribute endpoint)
- Swagger/OpenAPI UI with a built-in "Authorize" flow for testing JWT-protected routes
- EF Core Code-First migrations with seeded sample data

## Tech Stack

- **.NET 8** / ASP.NET Core Web API
- **Entity Framework Core 8** (SQL Server provider)
- **AutoMapper** for entity ↔ DTO mapping
- **FluentValidation** for request validation
- **JWT Bearer Authentication** (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Swashbuckle / Swagger** for API documentation

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server instance (local SQL Server / SQL Server Express / LocalDB)
- EF Core CLI tools (`dotnet tool install --global dotnet-ef` if not already installed)

## Project Structure (Clean Architecture)

```
Track Management/
├── Domain/                    # Enterprise-wide entities and enums, no dependencies
│   ├── Entities/               # Artist, Track, DSP, TrackDistribution
│   └── Enums/                  # TrackStatus, DistributionStatus
├── Application/                # Business logic, orchestration
│   ├── DTOs/                   # Auth, Artists, Tracks request/response models
│   ├── Interfaces/             # Service and repository abstractions
│   ├── Services/                # ArtistService, TrackService, JWT token contract, etc.
│   ├── Mappings/                # AutoMapper profiles
│   ├── Validators/              # FluentValidation validators
│   └── Common/Exceptions/       # NotFoundException, ConflictException
├── Infrastructure/             # EF Core, persistence, external concerns
│   ├── Persistence/             # AppDbContext, entity configurations, seed data
│   ├── Migrations/              # EF Core migrations
│   ├── Repositories/            # UnitOfWork / repository implementations
│   └── Auth/                    # JwtSettings, JwtTokenGenerator
└── Track Management/           # API host (composition root)
    ├── Controllers/             # ArtistsController, TracksController, AuthController
    ├── Filters/                 # ValidationFilter, AuthorizeCheckOperationFilter
    ├── Middleware/               # ExceptionMiddleware
    └── Program.cs                # App configuration and startup
```

Dependencies flow inward: `Track Management` → `Infrastructure`/`Application` → `Domain`.
The API project never talks to EF Core directly — it depends on `Application` interfaces,
which `Infrastructure` implements.

## Configuration

Configuration lives in `Track Management/appsettings.json` (base settings) and
`appsettings.Development.json` (environment overrides).

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TrackManagement;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "ThisIsADevelopmentOnlySigningKey_ReplaceInProduction_1234567890",
    "Issuer": "TrackManagementApi",
    "Audience": "TrackManagementClient",
    "ExpiryMinutes": 60
  },
  "AuthCredentials": {
    "Username": "admin",
    "Password": "Admin@123"
  }
}
```

- **`ConnectionStrings:DefaultConnection`** — update `Server=` to point at your own SQL
  Server instance before running migrations.
- **`Jwt`** — signing key, issuer, audience and token expiry used to issue and validate JWTs.
  Replace `Jwt:Key` with a strong secret before any non-local use.
- **`AuthCredentials`** — the single set of credentials accepted by the login endpoint in
  this sample (there is no user database/registration flow).

## Backend Setup

1. Restore dependencies and build the solution:
   ```bash
   dotnet restore "Track Management.sln"
   dotnet build "Track Management.sln"
   ```
2. Update `ConnectionStrings:DefaultConnection` in
   `Track Management/appsettings.json` to point to your SQL Server instance.
3. Apply EF Core migrations (see below) to create and seed the database.

## Applying EF Core Migrations

Migrations are defined in the `Infrastructure` project and targeted at the `Track Management`
startup project. Run these from the solution root (`Track-Management-Backend/`):

```bash
dotnet ef database update --project Infrastructure --startup-project "Track Management"
```

This creates the `TrackManagement` database (per the connection string) and applies the
existing `InitialMigration`, which also seeds:

- 3 artists
- 3 DSPs (Spotify, Apple Music, Amazon Music)
- 8 tracks across multiple genres (Pop, Electronic, Rock, Indie, Jazz) and statuses
  (Draft, Submitted, Distributed)
- 9 track-to-DSP distribution records (Pending, Live, Rejected)

To add a new migration after changing entities/configurations:

```bash
dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project "Track Management"
```

## How to Run the Project

From the solution root:

```bash
dotnet run --project "Track Management"
```

By default (see `Properties/launchSettings.json`) the API listens on:

- `http://localhost:5101`
- `https://localhost:7091`

In the `Development` environment, Swagger UI is available at `/swagger` (e.g.
`https://localhost:7091/swagger`).

CORS is configured to allow the Angular dev server at `http://localhost:4200`.

## How to Obtain a JWT Token

Authentication is a simple credential check against the `AuthCredentials` configuration
section (no user database) — intended for demoing the JWT-protected endpoint.

**Endpoint:** `POST /api/auth/token`

**Request body:**
```json
{
  "username": "admin",
  "password": "Admin@123"
}
```

**Response (200 OK):**
```json
{
  "token": "<jwt-token>",
  "expiresAt": "2026-07-19T12:00:00Z"
}
```

If the credentials don't match, the endpoint returns `401 Unauthorized`.

**Using the token:** send it as a Bearer token on protected endpoints:

```
Authorization: Bearer <jwt-token>
```

You can also authenticate directly from Swagger UI: click **Authorize**, paste
`Bearer <jwt-token>`, and call any endpoint marked with a lock icon.

## API Overview

| Method | Route                         | Auth | Description                                              |
|--------|-------------------------------|------|------------------------------------------------------------|
| POST   | `/api/auth/token`             | —    | Exchange credentials for a JWT                            |
| POST   | `/api/artists`                | —    | Create an artist                                          |
| GET    | `/api/artists`                | —    | List all artists                                          |
| POST   | `/api/tracks`                 | —    | Create a track for an artist                              |
| GET    | `/api/tracks`                 | —    | List tracks, filterable by `artistId`, `genre`, `status`  |
| GET    | `/api/tracks/{id}`            | —    | Get track details, including its DSP distribution statuses|
| POST   | `/api/tracks/{id}/distribute` | JWT  | Submit a track to one or more DSPs                        |
| PATCH  | `/api/tracks/{id}/status`     | —    | Update a track's status                                   |

Request bodies are validated with FluentValidation; invalid requests return
`400 Bad Request` with a field-level error dictionary (via ASP.NET Core's `ModelState`).
Unhandled domain errors are mapped by `ExceptionMiddleware` to `404 Not Found`
(`NotFoundException`) or `409 Conflict` (`ConflictException`), with a JSON body of the
shape `{ "status": <code>, "message": "<reason>" }`.

### Example: distribute a track

```
POST /api/tracks/1/distribute
Authorization: Bearer <jwt-token>
Content-Type: application/json

{
  "dspIds": [1, 2]
}
```

### Example: update track status

```
PATCH /api/tracks/1/status
Content-Type: application/json

{
  "status": "Submitted"
}
```

## Notes

- Seed data is defined in `Infrastructure/Persistence/Seed/SeedData.cs` and applied via
  EF Core's `HasData` in `AppDbContext.OnModelCreating`, so it is baked into the
  `InitialMigration` — running `dotnet ef database update` both creates the schema and
  seeds the data described above.
- The JWT signing key and `AuthCredentials` in `appsettings.json` are for local development
  only; replace them before deploying anywhere shared.
- Enum values (`TrackStatus`, `DistributionStatus`) are serialized/deserialized as strings
  (e.g. `"Draft"`, `"Live"`) via a `JsonStringEnumConverter`.

# New Zealand Walks API

An ASP.NET Core Web API that manages New Zealand regions and walking tracks, with ASP.NET Core Identity users, role-based JWT authorization and image uploads.

## Tech Stack

- .NET 8 (`net8.0`)
- ASP.NET Core Web API
- Entity Framework Core 8 with SQL Server
- ASP.NET Core Identity (`Microsoft.AspNetCore.Identity.EntityFrameworkCore`) with JWT bearer authentication
- AutoMapper 13
- Serilog (console and file sinks)
- Swashbuckle (Swagger UI with a Bearer token input)

## Project Structure

The solution `NZWALKSAPP.sln` contains a single project, `NZWALKS`:

| Folder | Description |
| --- | --- |
| `Controllers` | `AuthController`, `RegionsController`, `WalkController` and `ImagesController`. |
| `DB` | `NzdbContext` (regions, walks, difficulties, images, with seed data) and `AuthDBContext` (Identity, seeds the `Reader` and `Writer` roles). |
| `IRepository` | Repository interfaces and their implementations (regions, walks, local image upload, JWT creation). |
| `Models` | Domain models and DTOs. |
| `Mappings` | AutoMapper profile. |
| `CustomAFilter` | `ValidateModel` action filter that returns 400 for an invalid model state. |
| `Exceptions` | Global exception-handling middleware. |
| `Migrations` | EF Core migrations for both DbContexts (`Migrations` and `Migrations/AuthDB`). |

## Features

- User registration with roles and login that returns a JWT (15-minute lifetime).
- Role-based authorization on regions: `Reader` can read, `Writer` can create, update and delete.
- CRUD endpoints for regions and walks.
- Walk listing with filtering (`region` or `difficulty` name), sorting (`name` or `length`, ascending or descending) and paging.
- Image upload (`.jpg`, `.jpeg`, `.png`, `.gif`, up to 10 MB) saved to the `Images` folder, recorded in the database and served under `/Images`.
- Seed data for regions and difficulties.
- Global exception handling and Serilog logging.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Configuration

Settings are in `NZWALKS/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=np:localhost;Database=NZWalks;Integrated Security=true;TrustServerCertificate=True",
  "AuthConnection": "Server=np:localhost;Database=NZWalksAuth;Integrated Security=true;TrustServerCertificate=True"
},
"JWT": {
  "Key": "CHANGE_ME_use_user_secrets_or_env_var_min_32_chars",
  "issuer": "http://localhost:5232",
  "audience": "http://localhost:5232"
}
```

Adjust `Server=` to your SQL Server instance. `JWT:Key` is only a placeholder; set your own signing key (at least 32 characters) with user secrets or an environment variable:

```bash
cd NZWALKS
dotnet user-secrets init
dotnet user-secrets set "JWT:Key" "<a random string of at least 32 characters>"
```

or

```bash
# Linux/macOS
export JWT__Key="<a random string of at least 32 characters>"
# Windows PowerShell
$env:JWT__Key="<a random string of at least 32 characters>"
```

### Create the databases

```bash
cd NZWALKS
dotnet ef database update --context NzdbContext
dotnet ef database update --context AuthDBContext
```

### Run

```bash
cd NZWALKS
dotnet run --launch-profile https
```

The API listens on `https://localhost:7025` and `http://localhost:5232`; Swagger UI is at `/swagger`.

Register a user with the `Reader` and/or `Writer` role, log in, and send the returned token as `Authorization: Bearer <token>`.

## API Endpoints

### Auth

| Method | Route | Description |
| --- | --- | --- |
| POST | `/Register` | Register a user (`userName` as email, `password`, `roles`, e.g. `["Reader","Writer"]`). |
| POST | `/Login` | Log in (`userName`, `password`) and receive `{ "jwtToken": "..." }`. |

### Regions (requires JWT)

| Method | Route | Role | Description |
| --- | --- | --- | --- |
| GET | `/api/Regions` | Reader | Get all regions. |
| GET | `/api/Regions/{id}` | Reader | Get a region by id. |
| POST | `/api/Regions` | Writer | Create a region. |
| PUT | `/api/Regions/{id}` | Writer | Update a region. |
| DELETE | `/api/Regions/{id}` | Writer | Delete a region. |

### Walks

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/Walk` | List walks. Query: `filterOn` (`region`/`difficulty`), `filterQuery`, `SortingQuery` (`name`/`length`), `fromQuery` (ascending, default `true`), `pagenb`, `pagsize`. |
| GET | `/api/Walk/{id}` | Get a walk by id. |
| POST | `/api/Walk` | Create a walk. |
| PUT | `/api/Walk/{id}` | Update a walk. |
| DELETE | `/api/Walk/{id}` | Delete a walk. |

### Images

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/Images/upload` | Upload an image (`multipart/form-data`: `File`, `FileName`, `FileDescription`). |

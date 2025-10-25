# IGCSE Learning Hub

Backend Web API for an IGCSE learning platform, following a clean architecture split into Domain, Application, Infrastructure, and WebAPI layers.

## Tech Stack
- .NET 8 Web API, ASP.NET Core
- EF Core (SQL Server), code‑first migrations
- API Versioning, Swagger/OpenAPI
- JWT authentication, role‑based authorization
- Serilog structured logging

## Prerequisites
- .NET 8 SDK
- SQL Server (local or remote)

## Configuration
Configure via environment variables or user secrets (recommended for local dev). Required keys:

- `ConnectionStrings__IGCSELearningHub_DB` – SQL Server connection string
- `Authentication__Jwt__Secret` – strong secret for HMAC signing
- `Authentication__Jwt__Issuer` – token issuer
- `Authentication__Jwt__Audience` – token audience
- `Vnpay__vnp_TmnCode`, `Vnpay__vnp_HashSecret`, `Vnpay__vnp_BaseUrl`, `Vnpay__vnp_ReturnUrl` – VNPay settings (section name: `Vnpay`)

Example (PowerShell) using user-secrets from the `WebAPI` project folder:

```
dotnet user-secrets set "ConnectionStrings:IGCSELearningHub_DB" "Server=(local);Database=IGCSELearningHubDB;Trusted_Connection=True;TrustServerCertificate=true;"
dotnet user-secrets set "Authentication:Jwt:Secret" "<your-strong-secret>"
dotnet user-secrets set "Authentication:Jwt:Issuer" "IGCSELearningHubApp"
dotnet user-secrets set "Authentication:Jwt:Audience" "IGCSELearningHubUsers"
dotnet user-secrets set "Vnpay:vnp_TmnCode" "<tmn>"
dotnet user-secrets set "Vnpay:vnp_HashSecret" "<hash>"
dotnet user-secrets set "Vnpay:vnp_BaseUrl" "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"
dotnet user-secrets set "Vnpay:vnp_ReturnUrl" "https://localhost:5001/api/v1/vnpay/callback"
```

## Database
From the `WebAPI` directory:

```
dotnet ef database update
```

If running EF Core tools from the `Infrastructure` project, ensure the connection string is provided via env var `ConnectionStrings__IGCSELearningHub_DB`.

## Run
From `WebAPI`:

```
dotnet run
```

Browse Swagger UI at `/swagger`.

## Notes
- CORS is currently wide open for all origins; restrict it for production.
- Do not store secrets in `appsettings.json`; prefer environment variables or user-secrets.

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
- `Firebase__CredentialsPath` **or** env var `FIREBASE_CREDENTIALS` – Firebase admin key (JSON). Azure App Service can use the env var instead of a file.
- `Email__Host`, `Email__Port`, `Email__From`, `Email__UserName`, `Email__Password`, `Email__EnableSsl`, `Email__DisplayName` – SMTP settings; Azure overrides via `Email__*` env vars.

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
dotnet user-secrets set "Firebase:CredentialsPath" "Infrastructure/Firebase/igcse-learning-hub-firebase-adminsdk.json"
dotnet user-secrets set "Email:Host" "smtp.example.com"
dotnet user-secrets set "Email:Port" "587"
dotnet user-secrets set "Email:From" "no-reply@example.com"
dotnet user-secrets set "Email:UserName" "smtp-user"
dotnet user-secrets set "Email:Password" "smtp-password"
dotnet user-secrets set "Email:EnableSsl" "true"
```

When deploying, you can set `FIREBASE_CREDENTIALS` with the raw JSON string instead of a file.

## Database
From the `WebAPI` directory:

```
dotnet ef database update
```

If running EF Core tools from the `Infrastructure` project, ensure the connection string is provided via env var `ConnectionStrings__IGCSELearningHub_DB`.

Design-time connection resolution uses the current working directory. Either:
- Set env var in the same terminal: `$env:ConnectionStrings__IGCSELearningHub_DB = '<your-connection>'`
- Or run EF from `WebAPI/` so it reads `WebAPI/appsettings.json`.

Common commands (PowerShell):
- Add migration: `dotnet ef migrations add <Name> --project Infrastructure --context AppDbContext --output-dir Migrations`
- Update DB: `dotnet ef database update --project Infrastructure --context AppDbContext`

## Run
From `WebAPI`:

```
dotnet run
```

Browse Swagger UI at `/swagger`.

## Authentication
- Access token: short-lived JWT (default 15 minutes), returned as `accessToken`.
- Refresh token: long-lived, persisted in DB, returned as `refreshToken` and rotated on refresh.
- Google login: `POST /api/v1/Authentication/google-login { idToken }` (Firebase ID token). Google accounts cannot reset password; they must log in via Firebase each time.

Endpoints
- `POST /api/v1/Authentication/register` – returns `{ accessToken, refreshToken, ... }`
- `POST /api/v1/Authentication/login` – returns `{ accessToken, refreshToken, ... }`
- `POST /api/v1/Authentication/refresh` – body `{ refreshToken }`, returns new `{ accessToken, refreshToken }`
- `POST /api/v1/Authentication/revoke` – body `{ refreshToken, reason? }`, revokes one session
- `POST /api/v1/Authentication/revoke-all` – body `{ reason? }`, revokes all sessions for current user

Client flow
- Use `Authorization: Bearer <accessToken>` on API calls.
- When 401/expired, call `refresh` and replace both tokens.

### Device registration
Flutter clients should register FCM tokens so backend can send push notifications:

- `POST /api/v1/devices/sync` – body `{ deviceToken, deviceType, deviceName?, osVersion?, appVersion? }`
- `POST /api/v1/devices/unregister` – body `{ deviceToken }`
- `GET /api/v1/devices/my` – list active devices for the current user

Tokens are stored per account and used for payment notifications/push. Call `sync` after login and `unregister` on logout.

## Public Discovery APIs
- Livestreams
  - `GET /api/v1/livestreams?courseId=&from=&to=&sort=&pageNumber=&pageSize=` – list upcoming
  - `GET /api/v1/livestreams/{id}` – detail
- Packages
  - `GET /api/v1/packages?q=&sort=&pageNumber=&pageSize=` – list
  - `GET /api/v1/packages/{id}` – detail

## Payments (VNPay)
- ReturnUrl (versioned, lowercase): `https://<host>/api/v1/vnpay/callback`
- Callback handling is idempotent; repeated callbacks will not duplicate payments.
- After VNPay callback succeeds, server auto-enrolls the order and
  - Sends Firebase push “Thanh toán thành công, khóa học đã được mở” to all active devices for that user (make sure each client calls the device sync API so tokens exist).
  - Emits a (future) SignalR realtime event placeholder (`IPaymentRealtimeNotifier`).

## Notes
- CORS is currently wide open for all origins; restrict it for production.
- Do not store secrets in `appsettings.json`; prefer environment variables or user-secrets.
- Firebase push relies on stored device tokens; ensure mobile/web clients call the device sync APIs.

# MotoCare — Moto-Taxi Maintenance Tracker

ASP.NET Core Razor Pages, C#, EF Core + SQL Server.

## Running it locally

1. Open the solution in Visual Studio (or `dotnet run` from this folder).
2. Make sure SQL Server LocalDB is installed (comes with Visual Studio's ASP.NET workload).
3. The connection string in `appsettings.json` points at `(localdb)\mssqllocaldb` and a
   `MotoCareDb` database — change it if you're using a different SQL Server instance.
4. On first run, the app creates the database and tables automatically
   (`Database.EnsureCreated()` in `Program.cs`). There's no seed data yet, so the
   **Motorcycles** and **Admin > Manage Users** pages will look empty until you add a user.

## Branch / module layout (4-person split)

| Branch | Owner | Covers |
|---|---|---|
| `auth` | Member 1 | Login/Register/roles, `User` model + home page |
| `motorcycles` | Member 2 | Motorcycles + Garages CRUD, Admin > Manage Users, search by plate |
| `repairs` | Member 3 | Service requests + repairs |
| `parts-reports` | Member 4 | Spare parts, costs, reports |

Note for Member 1: `Models/User.cs` and the `Users` table already exist (added on the
`motorcycles` branch as a scaffold, since the Admin "manage users" page and the
Motorcycle → Owner relationship both need it) — extend it rather than recreating it when
you build Register/Login.

Once everyone's schema has settled, switch `Program.cs` from `Database.EnsureCreated()`
to EF Core Migrations (`dotnet ef migrations add InitialCreate`, then `Database.Migrate()`).

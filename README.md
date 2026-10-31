# League Manager (ASP.NET Core MVC)

A small ASP.NET Core 8 MVC web app for managing a recreational sports league's teams and players.

## Controllers

| Controller | Purpose | Routes |
|---|---|---|
| `HomeController` | Landing page, privacy page, error page | `/`, `/Home/Privacy` |
| `TeamsController` | Create, view, edit and delete teams; view a team's roster | `/Teams`, `/Teams/Details/{id}`, `/Teams/Create`, `/Teams/Edit/{id}`, `/Teams/Delete/{id}` |
| `PlayersController` | Create, edit and delete players; filter players by team | `/Players`, `/Players?teamId={id}`, `/Players/Create`, `/Players/Edit/{id}`, `/Players/Delete/{id}` |

## Project structure

- `Controllers/` – MVC controllers
- `Models/` – `Team` and `Player` models with data-annotation validation
- `Services/LeagueRepository.cs` – thread-safe in-memory data store (registered as a singleton), seeded with sample data
- `Views/` – Razor views for each controller

Data is kept in memory, so no database setup is needed; it resets each time the app restarts.

## Running locally

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

Then open the URL shown in the terminal (for example `http://localhost:5000`).

## Branches

- `main` – stable code
- `feature/players` – development branch where the Players feature was built before being merged into `main`

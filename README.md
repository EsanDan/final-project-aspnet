# League Manager (ASP.NET Core MVC)

For my final project, I built League Manager, an ASP.NET Core 8 MVC web app for managing the teams and players in a recreational sports league.

## Controllers

I created three controllers:

| Controller | What it does | Routes |
|---|---|---|
| `HomeController` | Shows the landing page, privacy page and error page | `/`, `/Home/Privacy` |
| `TeamsController` | Lets me create, view, edit and delete teams, and see each team's roster | `/Teams`, `/Teams/Details/{id}`, `/Teams/Create`, `/Teams/Edit/{id}`, `/Teams/Delete/{id}` |
| `PlayersController` | Lets me create, edit and delete players, and filter them by team | `/Players`, `/Players?teamId={id}`, `/Players/Create`, `/Players/Edit/{id}`, `/Players/Delete/{id}` |

## How I structured the project

- `Controllers/` – my MVC controllers
- `Models/` – the `Team` and `Player` models, which use data annotations for validation
- `Services/LeagueRepository.cs` – an in-memory data store I registered as a singleton and seeded with sample data
- `Views/` – the Razor views for each controller

I kept the data in memory so the app runs without any database setup. Because of that, the data resets every time the app restarts.

## Running it

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download). From the project folder, run:

```bash
dotnet run
```

Then open the URL shown in the terminal (for example `http://localhost:5000`).

## Branches

- `main` – my stable code
- `feature/players` – the branch where I built the Players feature before merging it into `main`

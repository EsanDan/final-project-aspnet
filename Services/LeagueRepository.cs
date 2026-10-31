using LeagueManager.Models;

namespace LeagueManager.Services;

/// <summary>
/// Simple thread-safe in-memory data store so the app runs without a database.
/// Registered as a singleton in Program.cs.
/// </summary>
public class LeagueRepository
{
    private readonly List<Team> _teams = new();
    private readonly List<Player> _players = new();
    private readonly object _lock = new();
    private int _nextTeamId = 1;
    private int _nextPlayerId = 1;

    public LeagueRepository()
    {
        AddTeam(new Team { Name = "Buckeyes", City = "Columbus", Wins = 8, Losses = 2 });
        AddTeam(new Team { Name = "Bearcats", City = "Cincinnati", Wins = 7, Losses = 3 });
        AddTeam(new Team { Name = "Rockets", City = "Toledo", Wins = 4, Losses = 6 });

        AddPlayer(new Player { FirstName = "Marcus", LastName = "Hill", Position = "Captain", TeamId = 1 });
        AddPlayer(new Player { FirstName = "Ava", LastName = "Turner", Position = "Forward", TeamId = 1 });
        AddPlayer(new Player { FirstName = "Jordan", LastName = "Lee", Position = "Goalkeeper", TeamId = 2 });
        AddPlayer(new Player { FirstName = "Sam", LastName = "Patel", Position = "Defender", TeamId = 3 });
    }

    // ---------- Teams ----------
    public IReadOnlyList<Team> GetTeams()
    {
        lock (_lock) return _teams.OrderBy(t => t.Name).ToList();
    }

    public Team? GetTeam(int id)
    {
        lock (_lock) return _teams.FirstOrDefault(t => t.Id == id);
    }

    public void AddTeam(Team team)
    {
        lock (_lock)
        {
            team.Id = _nextTeamId++;
            _teams.Add(team);
        }
    }

    public bool UpdateTeam(Team team)
    {
        lock (_lock)
        {
            var existing = _teams.FirstOrDefault(t => t.Id == team.Id);
            if (existing is null) return false;
            existing.Name = team.Name;
            existing.City = team.City;
            existing.Wins = team.Wins;
            existing.Losses = team.Losses;
            return true;
        }
    }

    public bool DeleteTeam(int id)
    {
        lock (_lock)
        {
            var removed = _teams.RemoveAll(t => t.Id == id) > 0;
            if (removed) _players.RemoveAll(p => p.TeamId == id);
            return removed;
        }
    }

    // ---------- Players ----------
    public IReadOnlyList<Player> GetPlayers(int? teamId = null)
    {
        lock (_lock)
        {
            return _players
                .Where(p => teamId is null || p.TeamId == teamId)
                .OrderBy(p => p.LastName)
                .ToList();
        }
    }

    public Player? GetPlayer(int id)
    {
        lock (_lock) return _players.FirstOrDefault(p => p.Id == id);
    }

    public void AddPlayer(Player player)
    {
        lock (_lock)
        {
            player.Id = _nextPlayerId++;
            _players.Add(player);
        }
    }

    public bool UpdatePlayer(Player player)
    {
        lock (_lock)
        {
            var existing = _players.FirstOrDefault(p => p.Id == player.Id);
            if (existing is null) return false;
            existing.FirstName = player.FirstName;
            existing.LastName = player.LastName;
            existing.Position = player.Position;
            existing.TeamId = player.TeamId;
            return true;
        }
    }

    public bool DeletePlayer(int id)
    {
        lock (_lock) return _players.RemoveAll(p => p.Id == id) > 0;
    }
}

using System.ComponentModel.DataAnnotations;

namespace LeagueManager.Models;

public class Team
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string City { get; set; } = string.Empty;

    [Range(0, 100)]
    public int Wins { get; set; }

    [Range(0, 100)]
    public int Losses { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace LeagueManager.Models;

public class Player
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(60)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Position { get; set; } = string.Empty;

    [Display(Name = "Team")]
    public int TeamId { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}

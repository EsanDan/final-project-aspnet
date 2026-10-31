using LeagueManager.Models;
using LeagueManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeagueManager.Controllers;

public class PlayersController : Controller
{
    private readonly LeagueRepository _repo;

    public PlayersController(LeagueRepository repo)
    {
        _repo = repo;
    }

    // GET: /Players  or  /Players?teamId=2
    public IActionResult Index(int? teamId)
    {
        ViewBag.TeamFilter = new SelectList(_repo.GetTeams(), nameof(Team.Id), nameof(Team.Name), teamId);
        ViewBag.TeamNames = _repo.GetTeams().ToDictionary(t => t.Id, t => t.Name);
        return View(_repo.GetPlayers(teamId));
    }

    // GET: /Players/Create
    public IActionResult Create()
    {
        LoadTeams();
        return View(new Player());
    }

    // POST: /Players/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Player player)
    {
        ValidateTeam(player);
        if (!ModelState.IsValid)
        {
            LoadTeams(player.TeamId);
            return View(player);
        }
        _repo.AddPlayer(player);
        TempData["Message"] = $"Player '{player.FullName}' added.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Players/Edit/5
    public IActionResult Edit(int id)
    {
        var player = _repo.GetPlayer(id);
        if (player is null) return NotFound();
        LoadTeams(player.TeamId);
        return View(player);
    }

    // POST: /Players/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Player player)
    {
        if (id != player.Id) return BadRequest();
        ValidateTeam(player);
        if (!ModelState.IsValid)
        {
            LoadTeams(player.TeamId);
            return View(player);
        }
        if (!_repo.UpdatePlayer(player)) return NotFound();
        TempData["Message"] = $"Player '{player.FullName}' updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Players/Delete/5
    public IActionResult Delete(int id)
    {
        var player = _repo.GetPlayer(id);
        if (player is null) return NotFound();
        return View(player);
    }

    // POST: /Players/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_repo.DeletePlayer(id)) return NotFound();
        TempData["Message"] = "Player removed.";
        return RedirectToAction(nameof(Index));
    }

    private void LoadTeams(int? selected = null)
    {
        ViewBag.Teams = new SelectList(_repo.GetTeams(), nameof(Team.Id), nameof(Team.Name), selected);
    }

    private void ValidateTeam(Player player)
    {
        if (_repo.GetTeam(player.TeamId) is null)
        {
            ModelState.AddModelError(nameof(Player.TeamId), "Please choose a valid team.");
        }
    }
}

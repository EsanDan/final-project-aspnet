using LeagueManager.Models;
using LeagueManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeagueManager.Controllers;

public class TeamsController : Controller
{
    private readonly LeagueRepository _repo;

    public TeamsController(LeagueRepository repo)
    {
        _repo = repo;
    }

    // GET: /Teams
    public IActionResult Index()
    {
        return View(_repo.GetTeams());
    }

    // GET: /Teams/Details/5
    public IActionResult Details(int id)
    {
        var team = _repo.GetTeam(id);
        if (team is null) return NotFound();
        ViewBag.Players = _repo.GetPlayers(id);
        return View(team);
    }

    // GET: /Teams/Create
    public IActionResult Create()
    {
        return View(new Team());
    }

    // POST: /Teams/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Team team)
    {
        if (!ModelState.IsValid) return View(team);
        _repo.AddTeam(team);
        TempData["Message"] = $"Team '{team.Name}' created.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Teams/Edit/5
    public IActionResult Edit(int id)
    {
        var team = _repo.GetTeam(id);
        if (team is null) return NotFound();
        return View(team);
    }

    // POST: /Teams/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Team team)
    {
        if (id != team.Id) return BadRequest();
        if (!ModelState.IsValid) return View(team);
        if (!_repo.UpdateTeam(team)) return NotFound();
        TempData["Message"] = $"Team '{team.Name}' updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Teams/Delete/5
    public IActionResult Delete(int id)
    {
        var team = _repo.GetTeam(id);
        if (team is null) return NotFound();
        return View(team);
    }

    // POST: /Teams/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_repo.DeleteTeam(id)) return NotFound();
        TempData["Message"] = "Team deleted.";
        return RedirectToAction(nameof(Index));
    }
}

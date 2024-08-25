// TeamController.cs
using Microsoft.AspNetCore.Mvc;
using TASK_2.Models;
using TASK_2.DTOs;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TeamController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllTeams()
    {
        var teams = _context.Teams.Select(t => new TeamDto
        {
            Id = t.Id,
            ProjectId = t.ProjectId,
            Name = t.Name
        }).ToList();

        return Ok(teams);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeamById(int id)
    {
        var team = _context.Teams.FirstOrDefault(t => t.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        return Ok(new TeamDto
        {
            Id = team.Id,
            ProjectId = team.ProjectId,
            Name = team.Name
        });
    }

    [HttpPost]
    public IActionResult CreateTeam([FromBody] CreateTeamDto teamDto)
    {
        var team = new Team
        {
            ProjectId = teamDto.ProjectId,
            Name = teamDto.Name
        };

        _context.Teams.Add(team);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, new TeamDto { Id = team.Id, ProjectId = team.ProjectId, Name = team.Name });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTeam(int id, [FromBody] UpdateTeamDto teamDto)
    {
        var team = _context.Teams.FirstOrDefault(t => t.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        team.ProjectId = teamDto.ProjectId;
        team.Name = teamDto.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeam(int id)
    {
        var team = _context.Teams.FirstOrDefault(t => t.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        _context.Teams.Remove(team);
        _context.SaveChanges();

        return NoContent();
    }
}

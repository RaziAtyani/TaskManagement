// TeamMemberController.cs
using Microsoft.AspNetCore.Mvc;
using TASK_2.Models;
using TASK_2.DTOs;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TeamMemberController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllTeamMembers()
    {
        var teamMembers = _context.TeamMembers.Select(tm => new TeamMemberDto()
        {
            Id = tm.Id,
            TeamId = tm.TeamId,
            UserId = tm.UserId,
            RoleId = tm.RoleId
        }).ToList();

        return Ok(teamMembers);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeamMemberById(int id)
    {
        var teamMember = _context.TeamMembers.FirstOrDefault(tm => tm.Id == id);
        if (teamMember == null) return NotFound();

        return Ok(teamMember);
    }

    [HttpPost]
    public IActionResult CreateTeamMember(CreateTeamMemberDto tmDto)
    {
        var teamMember = new TeamMember
        {
            TeamId = tmDto.TeamId,
            UserId = tmDto.UserId,
            RoleId = tmDto.RoleId
        };

        _context.TeamMembers.Add(teamMember);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTeamMemberById), new { id = teamMember.Id }, teamMember);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTeamMember(int id, UpdateTeamMemberDto tmDto)
    {
        var teamMember = _context.TeamMembers.FirstOrDefault(tm => tm.Id == id);
        if (teamMember == null) return NotFound();

        teamMember.TeamId = tmDto.TeamId;
        teamMember.UserId = tmDto.UserId;
        teamMember.RoleId = tmDto.RoleId;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeamMember(int id)
    {
        var teamMember = _context.TeamMembers.FirstOrDefault(tm => tm.Id == id);
        if (teamMember == null) return NotFound();

        _context.TeamMembers.Remove(teamMember);
        _context.SaveChanges();

        return NoContent();
    }
}

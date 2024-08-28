using Microsoft.AspNetCore.Mvc;
using TASK_2.DTOs;
using TASK_2.Models;
using TASK_2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    public TeamMemberController(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetAllTeamMembers()
    {
        var teamMembers = await _teamMemberService.GetAllTeamMembersAsync();
        return Ok(teamMembers.Select(tm => new TeamMemberDto
        {
            Id = tm.Id,
            UserId = tm.UserId,
            TeamId = tm.TeamId,
            RoleId = tm.RoleId
        }).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamMemberDto>> GetTeamMemberById(int id)
    {
        var teamMember = await _teamMemberService.GetTeamMemberByIdAsync(id);
        if (teamMember == null)
        {
            return NotFound();
        }

        return Ok(new TeamMemberDto
        {
            Id = teamMember.Id,
            UserId = teamMember.UserId,
            TeamId = teamMember.TeamId,
            RoleId = teamMember.RoleId
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddTeamMember([FromBody] CreateTeamMemberDto teamMemberDto)
    {
        var newTeamMember = await _teamMemberService.AddTeamMemberAsync(new TeamMember
        {
            UserId = teamMemberDto.UserId,
            TeamId = teamMemberDto.TeamId,
            RoleId = teamMemberDto.RoleId
        });

        return CreatedAtAction(nameof(GetTeamMemberById), new { id = newTeamMember.Id }, newTeamMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] UpdateTeamMemberDto teamMemberDto)
    {
        var teamMember = await _teamMemberService.GetTeamMemberByIdAsync(id);
        if (teamMember == null)
        {
            return NotFound();
        }

        teamMember.UserId = teamMemberDto.UserId;
        teamMember.TeamId = teamMemberDto.TeamId;
        teamMember.RoleId = teamMemberDto.RoleId;

        await _teamMemberService.UpdateTeamMemberAsync(teamMember);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamMember(int id)
    {
        if (!await _teamMemberService.TeamMemberExists(id))
        {
            return NotFound();
        }

        await _teamMemberService.DeleteTeamMemberAsync(id);
        return NoContent();
    }

    [HttpGet("ByTeam/{teamId}")]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetTeamMembersByTeamId(int teamId)
    {
        var teamMembers = await _teamMemberService.GetTeamMembersByTeamIdAsync(teamId);
        if (teamMembers == null || !teamMembers.Any())
        {
            return NotFound($"No team members found for team ID {teamId}.");
        }

        return Ok(teamMembers.Select(tm => new TeamMemberDto
        {
            Id = tm.Id,
            UserId = tm.UserId,
            TeamId = tm.TeamId,
            RoleId = tm.RoleId
        }).ToList());
    }
}

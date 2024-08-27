using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TASK_2.Common;
using TASK_2.DTOs;
using TASK_2.Repositories;
using TASK_2.Services;

namespace TASK_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IProjectRepository _projectRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamMembersController(ITeamMemberService teamMemberService, IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor)
        {
            _teamMemberService = teamMemberService;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private async Task<bool> UserIsAuthorizedToManageTeam(int teamId)
        {
            var userId = GetCurrentUserId();
            var project = await _projectRepository.GetProjectByTeamIdAsync(teamId);

            return project != null && (project.RegistrationId == userId || User.IsInRole("Admin"));
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamMember([FromBody] TeamMemberDto teamMemberDto)
        {
            if (!await UserIsAuthorizedToManageTeam(teamMemberDto.TeamId))
            {
                return Forbid("You are not authorized to add team members to this team.");
            }

            var result = await _teamMemberService.AddTeamMemberAsync(teamMemberDto);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetTeamMemberById), new { id = result.Data.Id }, result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeamMembers()
        {
            var result = await _teamMemberService.GetAllTeamMembersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            var result = await _teamMemberService.GetTeamMemberByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] TeamMemberDto teamMemberDto)
        {
            if (!await UserIsAuthorizedToManageTeam(teamMemberDto.TeamId))
            {
                return Forbid("You are not authorized to update team members in this team.");
            }

            var result = await _teamMemberService.UpdateTeamMemberAsync(id, teamMemberDto);
            if (result.IsSuccess)
                return NoContent();

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _teamMemberService.GetTeamMemberByIdAsync(id);
            if (!await UserIsAuthorizedToManageTeam(teamMember.Data.TeamId))
            {
                return Forbid("You are not authorized to delete team members from this team.");
            }

            var result = await _teamMemberService.DeleteTeamMemberAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}

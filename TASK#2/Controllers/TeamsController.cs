using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Common;
using TASK_2.DTOs;
using TASK_2.Services;

namespace TASK_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] TeamDto teamDto)
        {
            var result = await _teamService.CreateTeamAsync(teamDto);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetTeamById), new { id = result.Data.Id }, result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var result = await _teamService.GetAllTeamsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var result = await _teamService.GetTeamByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDto teamDto)
        {
            var result = await _teamService.UpdateTeamAsync(id, teamDto);
            if (result.IsSuccess)
                return NoContent();

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _teamService.DeleteTeamAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}

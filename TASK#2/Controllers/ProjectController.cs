using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using TASK_2.DTOs;
using TASK_2.Services;
using TASK_2.Common;

namespace TASK_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Apply authorization to all endpoints in this controller
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDtoRequest projectDto)
        {
            var result = await _projectService.CreateProjectAsync(projectDto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetProject), new { id = result.Data.Id }, result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectService.GetAllProjectsAsync();
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var result = await _projectService.GetProjectAsync(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto projectDto)
        {
            var result = await _projectService.UpdateProjectAsync(id, projectDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet("{projectId}/subprojects")]
        public async Task<IActionResult> GetSubProjects(int projectId)
        {
            var result = await _projectService.GetSubProjectsByProjectIdAsync(projectId);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("{parentProjectId}/subprojects")]
        public async Task<IActionResult> CreateSubProject(int parentProjectId, [FromBody] ProjectDtoRequest subProjectDto)
        {
            var result = await _projectService.CreateSubProjectAsync(parentProjectId, subProjectDto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetSubProject), new { parentProjectId, subProjectId = result.Data.Id }, result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet("{parentProjectId}/subprojects/{subProjectId}")]
        public async Task<IActionResult> GetSubProject(int parentProjectId, int subProjectId)
        {
            var result = await _projectService.GetProjectAsync(subProjectId);
            if (result.IsSuccess )
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{parentProjectId}/subprojects/{subProjectId}")]
        public async Task<IActionResult> UpdateSubProject(int parentProjectId, int subProjectId, [FromBody] ProjectDto subProjectDto)
        {
            var result = await _projectService.UpdateSubProjectAsync(subProjectId, subProjectDto);
            if (result.IsSuccess )
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{parentProjectId}/subprojects/{subProjectId}")]
        public async Task<IActionResult> DeleteSubProject(int parentProjectId, int subProjectId)
        {
            var result = await _projectService.DeleteSubProjectAsync(subProjectId);
            if (result.IsSuccess )
            {
                return NoContent();
            }
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}

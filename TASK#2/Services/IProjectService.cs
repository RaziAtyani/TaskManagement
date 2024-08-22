using TASK_2.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Common;

namespace TASK_2.Services
{
    public interface IProjectService
    {
        Task<OperationResult<ProjectDto>> CreateProjectAsync(ProjectDtoRequest projectDto);
        Task<OperationResult<ProjectDto>> GetProjectAsync(int projectId);
        Task<OperationResult<IEnumerable<ProjectDto>>> GetAllProjectsAsync();
        Task<OperationResult<ProjectDto>> UpdateProjectAsync(int projectId, ProjectDto projectDto);
        Task<OperationResult> DeleteProjectAsync(int projectId);

        Task<OperationResult<IEnumerable<ProjectDto>>> GetSubProjectsByProjectIdAsync(int projectId);
    }
}

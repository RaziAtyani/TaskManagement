using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.DTOs;
using TASK_2.Models;
using TASK_2.Repositories;
using TASK_2.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace TASK_2.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IRegistrationRepository _registrationRepository;

        private readonly ILogger<ProjectService> _logger;


        public ProjectService(IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IRegistrationRepository registrationRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
            _registrationRepository = registrationRepository;
            _logger = logger;
        }


        public async Task<OperationResult<ProjectDto>> CreateProjectAsync(ProjectDtoRequest projectDto)
{
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
    {
        return new OperationResult<ProjectDto>(401, "Unauthorized user.");
    }
            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            // Validate the Name
            if (string.IsNullOrWhiteSpace(projectDto.Name))
    {
        return new OperationResult<ProjectDto>(400, "Project name is required.");
    }

    // Validate the Description
    if (string.IsNullOrWhiteSpace(projectDto.Description))
    {
        return new OperationResult<ProjectDto>(400, "Project description is required.");
    }

    // Check for existing project with the same name or description for the same user
    var existingProjects = await _projectRepository.GetProjectsByUserIdAsync(userId);
    if (existingProjects.Any(p => p.Name == projectDto.Name || p.Description == projectDto.Description))
    {
        return new OperationResult<ProjectDto>(400, "A project with the same name or description already exists.");
    }

    // Verify if the RegistrationId exists
    var registrationExists = await _registrationRepository.ExistsAsync(userId);
    if (!registrationExists)
    {
        return new OperationResult<ProjectDto>(400, "Invalid Registration ID.");
    }

    var project = new Project
    {
        Name = projectDto.Name,
        Description = projectDto.Description,
        RegistrationId = userId, // Set the RegistrationId to the ID of the current user
        StartDate = DateTime.Now, // Automatically set the start date to the current date and time
    };

    await _projectRepository.AddAsync(project);

    var resultDto = new ProjectDto
    {
        Name = project.Name,
        Description = project.Description
    };

    return new OperationResult<ProjectDto>(201, "Project created successfully.", resultDto);
}



        public async Task<OperationResult<ProjectDto>> GetProjectAsync(int projectId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<ProjectDto>(401, "Unauthorized user.");
            }

            var project = await _projectRepository.GetProjectWithSubProjectsAsync(projectId);

            if (project == null)
            {
                return new OperationResult<ProjectDto>(404, "Project not found.");
            }

            // Check if the project belongs to the current user
            if (project.RegistrationId != userId)
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            var projectDto = new ProjectDto
            {
                Name = project.Name,
                Description = project.Description
            };

            return new OperationResult<ProjectDto>(200, "Project retrieved successfully.", projectDto);
        }


        public async Task<OperationResult<IEnumerable<ProjectDto>>> GetAllProjectsAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<IEnumerable<ProjectDto>>(401, "Unauthorized user.");
            }

            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);
            var projectDtos = projects.Select(p => new ProjectDto
            {
                Name = p.Name,
                Description = p.Description
            });

            return new OperationResult<IEnumerable<ProjectDto>>(200, "Projects retrieved successfully.", projectDtos);
        }


        public async Task<OperationResult<ProjectDto>> UpdateProjectAsync(int projectId, ProjectDto projectDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<ProjectDto>(401, "Unauthorized user.");
            }

            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                return new OperationResult<ProjectDto>(404, "Project not found.");
            }

            // Check if the project belongs to the current user
            if (project.RegistrationId != userId)
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            // Update project details
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;

            await _projectRepository.UpdateAsync(project);

            var updatedProjectDto = new ProjectDto
            {
                Name = project.Name,
                Description = project.Description
            };

            return new OperationResult<ProjectDto>(200, "Project updated successfully.", updatedProjectDto);
        }


        public async Task<OperationResult> DeleteProjectAsync(int projectId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult(401, "Unauthorized user.");
            }

            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult(403, "Access denied.");
            }

            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                return new OperationResult(404, "Project not found.");
            }

            // Check if the project belongs to the current user
            if (project.RegistrationId != userId)
            {
                return new OperationResult(403, "Access denied.");
            }

            // Check if the project has subprojects
            var subProjects = await _projectRepository.GetSubProjectsByProjectIdAsync(projectId);
            if (subProjects.Any())
            {
                return new OperationResult(400, "Cannot delete project with subprojects.");
            }

            await _projectRepository.DeleteAsync(projectId);

            return new OperationResult(200, "Project deleted successfully.");
        }




        public async Task<OperationResult<IEnumerable<ProjectDto>>> GetSubProjectsByProjectIdAsync(int parentProjectId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<IEnumerable<ProjectDto>>(401, "Unauthorized user.");
            }



            // Check if the parent project exists and belongs to the current user
            var parentProject = await _projectRepository.GetByIdAsync(parentProjectId);
            if (parentProject == null || parentProject.RegistrationId != userId)
            {
                return new OperationResult<IEnumerable<ProjectDto>>(404, "Parent project not found or access denied.");
            }

            var subProjects = await _projectRepository.GetSubProjectsByProjectIdAsync(parentProjectId);
            var subProjectDtos = subProjects.Select(sp => new ProjectDto
            {
                Name = sp.Name,
                Description = sp.Description
            });

            return new OperationResult<IEnumerable<ProjectDto>>(200, "Sub-projects retrieved successfully.", subProjectDtos);
        }


        public async Task<OperationResult<ProjectDto>> CreateSubProjectAsync(int parentProjectId, ProjectDtoRequest subProjectDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<ProjectDto>(401, "Unauthorized user.");
            }

            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            // Check if the parent project exists and belongs to the current user
            var parentProject = await _projectRepository.GetByIdAsync(parentProjectId);
            if (parentProject == null || parentProject.RegistrationId != userId)
            {
                return new OperationResult<ProjectDto>(404, "Parent project not found or access denied.");
            }

            // Check if a sub-project with the same name or description already exists under the same parent project
            var existingSubProjects = await _projectRepository.GetSubProjectsByProjectIdAsync(parentProjectId);
            if (existingSubProjects.Any(sp => sp.Name == subProjectDto.Name || sp.Description == subProjectDto.Description))
            {
                return new OperationResult<ProjectDto>(400, "A sub-project with the same name or description already exists.");
            }

            var subProject = new Project
            {
                Name = subProjectDto.Name,
                Description = subProjectDto.Description,
                RegistrationId = userId,
                StartDate = DateTime.Now, // Automatically set the start date to the current date and time
                ParentProjectId = parentProjectId // Set the parent project ID
            };

            await _projectRepository.AddAsync(subProject);

            var resultDto = new ProjectDto
            {
                Name = subProject.Name,
                Description = subProject.Description
            };

            return new OperationResult<ProjectDto>(201, "Sub-project created successfully.", resultDto);
        }

        public async Task<OperationResult<ProjectDto>> UpdateSubProjectAsync(int subProjectId, ProjectDto subProjectDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult<ProjectDto>(401, "Unauthorized user.");
            }

            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult<ProjectDto>(403, "Access denied.");
            }

            var subProject = await _projectRepository.GetByIdAsync(subProjectId);
            if (subProject == null || subProject.RegistrationId != userId)
            {
                return new OperationResult<ProjectDto>(404, "Sub-project not found or access denied.");
            }

            subProject.Name = subProjectDto.Name;
            subProject.Description = subProjectDto.Description;

            await _projectRepository.UpdateAsync(subProject);

            return new OperationResult<ProjectDto>(200, "Sub-project updated successfully.", subProjectDto);
        }


        public async Task<OperationResult> DeleteSubProjectAsync(int subProjectId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return new OperationResult(401, "Unauthorized user.");
            }

            var userRole = await _registrationRepository.GetRoleByUserIdAsync(userId);
            if (userRole == null || (userRole.Name != "Admin" && userRole.Name != "ProjectLead"))
            {
                return new OperationResult(403, "Access denied.");
            }

            var subProject = await _projectRepository.GetByIdAsync(subProjectId);
            if (subProject == null || subProject.RegistrationId != userId)
            {
                return new OperationResult(404, "Sub-project not found or access denied.");
            }

            // Check if the sub-project has any sub-projects of its own
            var childSubProjects = await _projectRepository.GetSubProjectsByProjectIdAsync(subProjectId);
            if (childSubProjects.Any())
            {
                return new OperationResult(400, "Cannot delete a sub-project that has child sub-projects.");
            }

            await _projectRepository.DeleteAsync(subProjectId);

            return new OperationResult(200, "Sub-project deleted successfully.");
        }




    }
}

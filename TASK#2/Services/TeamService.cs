using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Common;
using TASK_2.DTOs;
using TASK_2.Models;
using TASK_2.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TASK_2.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamService(ITeamRepository teamRepository, IProjectRepository projectRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private async Task<bool> UserIsAssociatedWithProject(int projectId)
        {
            var userId = GetCurrentUserId();
            var project = await _projectRepository.GetByIdAsync(projectId);
            return project != null && project.RegistrationId == userId;
        }

        public async Task<OperationResult<TeamResponseDto>> CreateTeamAsync(TeamDto teamDto)
        {
            if (!await UserIsAssociatedWithProject(teamDto.ProjectId))
            {
                return new OperationResult<TeamResponseDto>(403, "You are not authorized to create a team for this project.");
            }

            var team = _mapper.Map<Team>(teamDto);
            await _teamRepository.AddAsync(team);
            var teamResponseDto = _mapper.Map<TeamResponseDto>(team);
            return new OperationResult<TeamResponseDto>(201, "Team created successfully", teamResponseDto);
        }

        public async Task<OperationResult<IEnumerable<TeamResponseDto>>> GetAllTeamsAsync()
        {
            var userId = GetCurrentUserId();
            var teams = await _teamRepository.GetAllAsync();
            var userTeams = new List<Team>();

            foreach (var team in teams)
            {
                if (await UserIsAssociatedWithProject(team.ProjectId))
                {
                    userTeams.Add(team);
                }
            }

            var teamResponseDtos = _mapper.Map<IEnumerable<TeamResponseDto>>(userTeams);
            return new OperationResult<IEnumerable<TeamResponseDto>>(200, "Teams retrieved successfully", teamResponseDtos);
        }

        public async Task<OperationResult<TeamResponseDto>> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                return new OperationResult<TeamResponseDto>(404, "Team not found");

            if (!await UserIsAssociatedWithProject(team.ProjectId))
            {
                return new OperationResult<TeamResponseDto>(403, "You are not authorized to view this team.");
            }

            var teamResponseDto = _mapper.Map<TeamResponseDto>(team);
            return new OperationResult<TeamResponseDto>(200, "Team retrieved successfully", teamResponseDto);
        }

        public async Task<OperationResult> UpdateTeamAsync(int id, TeamDto teamDto)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                return new OperationResult(404, "Team not found");

            if (!await UserIsAssociatedWithProject(team.ProjectId))
            {
                return new OperationResult(403, "You are not authorized to update this team.");
            }

            _mapper.Map(teamDto, team);
            await _teamRepository.UpdateAsync(team);
            return new OperationResult(200, "Team updated successfully");
        }

        public async Task<OperationResult> DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                return new OperationResult(404, "Team not found");

            if (!await UserIsAssociatedWithProject(team.ProjectId))
            {
                return new OperationResult(403, "You are not authorized to delete this team.");
            }

            await _teamRepository.DeleteAsync(id);
            return new OperationResult(200, "Team deleted successfully");
        }
    }
}

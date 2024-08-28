using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.DTOs;
using TASK_2.Common;

namespace TASK_2.Services
{
    public interface ITeamService
    {
        Task<OperationResult<TeamResponseDto>> CreateTeamAsync(TeamDto teamDto);
        Task<OperationResult<IEnumerable<TeamResponseDto>>> GetAllTeamsAsync();
        Task<OperationResult<TeamResponseDto>> GetTeamByIdAsync(int id);
        Task<OperationResult> UpdateTeamAsync(int id, TeamDto teamDto);
        Task<OperationResult> DeleteTeamAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.DTOs;
using TASK_2.Common;

namespace TASK_2.Services
{
    public interface ITeamMemberService
    {
        Task<OperationResult<TeamMemberResponseDto>> AddTeamMemberAsync(TeamMemberDto teamMemberDto);
        Task<OperationResult<IEnumerable<TeamMemberResponseDto>>> GetAllTeamMembersAsync();
        Task<OperationResult<TeamMemberResponseDto>> GetTeamMemberByIdAsync(int id);
        Task<OperationResult> UpdateTeamMemberAsync(int id, TeamMemberDto teamMemberDto);
        Task<OperationResult> DeleteTeamMemberAsync(int id);
    }
}

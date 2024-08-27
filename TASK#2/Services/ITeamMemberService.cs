using TASK_2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITeamMemberService
{
    Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync();
    Task<TeamMember> GetTeamMemberByIdAsync(int id);
    Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId);
    Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember);
    Task UpdateTeamMemberAsync(TeamMember teamMember);
    Task DeleteTeamMemberAsync(int id);
    Task<bool> TeamMemberExists(int id);
}

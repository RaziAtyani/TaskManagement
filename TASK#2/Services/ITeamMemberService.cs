using TASK_2.Models;

public interface ITeamMemberService
{
    Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync();
    Task<TeamMember> GetTeamMemberByIdAsync(int id);
    Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember);
    Task UpdateTeamMemberAsync(TeamMember teamMember);
    Task DeleteTeamMemberAsync(int id);
}

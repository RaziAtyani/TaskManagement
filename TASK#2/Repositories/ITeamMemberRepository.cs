using TASK_2.Models;

public interface ITeamMemberRepository
{
    Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync();
    Task<TeamMember> GetTeamMemberByIdAsync(int id);
    Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember);
    Task UpdateTeamMemberAsync(TeamMember teamMember);
    Task DeleteTeamMemberAsync(int id);
}

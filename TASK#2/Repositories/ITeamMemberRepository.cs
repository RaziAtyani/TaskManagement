using TASK_2.Models;

namespace TASK_2.Repositories
{
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {
        Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId);
        Task<IEnumerable<TeamMember>> GetTeamMembersByUserIdAsync(int userId);
    }
}

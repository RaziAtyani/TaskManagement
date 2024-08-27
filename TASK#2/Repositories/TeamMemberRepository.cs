using Microsoft.EntityFrameworkCore;
using TASK_2.Models;

namespace TASK_2.Repositories
{
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Method to get team members by team ID
        public async Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId)
        {
            return await _dbSet.Where(tm => tm.TeamId == teamId)
                               .Include(tm => tm.User)
                               .Include(tm => tm.Role)
                               .ToListAsync();
        }

        // Method to get team members by user ID
        public async Task<IEnumerable<TeamMember>> GetTeamMembersByUserIdAsync(int userId)
        {
            return await _dbSet.Where(tm => tm.UserId == userId)
                               .Include(tm => tm.Team)
                               .Include(tm => tm.Role)
                               .ToListAsync();
        }

        // Other team member-specific methods can be added here
    }
}

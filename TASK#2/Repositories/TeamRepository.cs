using Microsoft.EntityFrameworkCore;
using TASK_2.Models;

namespace TASK_2.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(ApplicationDbContext context) : base(context)
        {
        }

        // You can add methods specific to Team management if needed
        public async Task<IEnumerable<Team>> GetTeamsByProjectIdAsync(int projectId)
        {
            return await _dbSet.Where(t => t.ProjectId == projectId).ToListAsync();
        }

        // Other team-specific methods can be added here
    }
}

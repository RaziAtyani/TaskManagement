using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASK_2.Models;

namespace TASK_2.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsWithSubProjectsAsync()
        {
            return await _dbSet.Include(p => p.SubProjects).ToListAsync();
        }

        public async Task<Project> GetProjectWithSubProjectsAsync(int projectId)
        {
            return await _dbSet.Include(p => p.SubProjects).FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<IEnumerable<Project>> GetSubProjectsByProjectIdAsync(int projectId)
        {
            return await _dbSet.Where(p => p.ParentProjectId == projectId).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(p => p.RegistrationId == userId)
                .Include(p => p.SubProjects) // Include subprojects if needed
                .ToListAsync();
        }
    }
}

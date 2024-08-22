using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Models;

namespace TASK_2.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsWithSubProjectsAsync();
        Task<Project> GetProjectWithSubProjectsAsync(int projectId);
        Task<IEnumerable<Project>> GetSubProjectsByProjectIdAsync(int projectId);
    }
}

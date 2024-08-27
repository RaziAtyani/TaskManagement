using TASK_2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TASK_2.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<IEnumerable<Team>> GetTeamsByProjectIdAsync(int projectId);
    }


}

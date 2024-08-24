using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TASK_2.models;
using TASK_2.Models;

namespace TASK_2.Repositories
{
    public class RegistrationRepository : GenericRepository<models.User>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.Username == username);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public models.User GetByUsername(string username)
        {
            return _dbSet.SingleOrDefault(u => u.Username == username);
        }
    }
}

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

        // Implementation of new methods
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task AddRegistrationAsync(Registration registration)
        {
            await _context.Registrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Registrations.AnyAsync(r => r.Id == id);
        }

        public async Task<Role> GetRoleByUserIdAsync(int userId)
        {
            return await _context.Registrations
                .Where(r => r.UserId == userId)
                .Select(r => r.Role)
                .SingleOrDefaultAsync();
        }

    }
}

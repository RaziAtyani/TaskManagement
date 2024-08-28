using TASK_2.Models;
using System.Threading.Tasks;
using TASK_2.models;
using TASK_2.Repositories;
using Microsoft.EntityFrameworkCore;

public interface IRegistrationRepository : IGenericRepository<User>
{
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    User GetByUsername(string username);

    // New methods
    Task<Role> GetRoleByNameAsync(string roleName);
    Task AddRegistrationAsync(Registration registration);
    Task<Role> GetRoleByUserIdAsync(int userId);

    Task<bool> ExistsAsync(int id);



}

using TASK_2.Models;
using System.Threading.Tasks;
using TASK_2.models;
using TASK_2.Repositories;

public interface IRegistrationRepository : IGenericRepository<TASK_2.models.User>
{
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    TASK_2.models.User GetByUsername(string username);
}

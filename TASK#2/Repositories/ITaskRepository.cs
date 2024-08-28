using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Models;

public interface ITaskRepository
{
    Task<IEnumerable<Tasks>> GetAllTasksAsync();
    Task<Tasks> GetTaskByIdAsync(int id);
    Task<Tasks> AddTaskAsync(Tasks task);
    Task UpdateTaskAsync(Tasks task);
    Task DeleteTaskAsync(int id);
}

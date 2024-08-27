using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Models;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<Tasks> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }

    public async Task<Tasks> AddTaskAsync(Tasks task)
    {
        return await _taskRepository.AddTaskAsync(task);
    }

    public async Task UpdateTaskAsync(Tasks task)
    {
        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteTaskAsync(id);
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Models;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
    {
        return await _context.Tasks
                             .Include(t => t.AssignedTo)
                             .Include(t => t.Project)
                             .Include(t => t.ParentTask)
                             .Include(t => t.SubTasks)
                             .ToListAsync();
    }

    public async Task<Tasks> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks
                             .Include(t => t.AssignedTo)
                             .Include(t => t.Project)
                             .Include(t => t.ParentTask)
                             .Include(t => t.SubTasks)
                             .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tasks> AddTaskAsync(Tasks task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task UpdateTaskAsync(Tasks task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}

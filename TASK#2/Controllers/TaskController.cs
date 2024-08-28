using Microsoft.AspNetCore.Mvc;
using TASK_2.Models;
using TASK_2.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  // For async operations
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
        var tasks = await _context.Tasks
                                  .Select(task => new TaskDto
                                  {
                                      Id = task.Id,
                                      Title = task.Title,
                                      Description = task.Description,
                                      AssignedToId = task.AssignedToId,
                                      ProjectId = task.ProjectId,
                                      ParentTaskId = task.ParentTaskId,
                                      Status = task.Status,
                                      DueDate = task.DueDate
                                  })
                                  .ToListAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(int id)
    {
        var task = await _context.Tasks
                                 .Where(t => t.Id == id)
                                 .Select(task => new TaskDto
                                 {
                                     Id = task.Id,
                                     Title = task.Title,
                                     Description = task.Description,
                                     AssignedToId = task.AssignedToId,
                                     ProjectId = task.ProjectId,
                                     ParentTaskId = task.ParentTaskId,
                                     Status = task.Status,
                                     DueDate = task.DueDate
                                 })
                                 .FirstOrDefaultAsync();

        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var task = new Tasks
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            AssignedToId = taskDto.AssignedToId,
            ProjectId = taskDto.ProjectId,
            ParentTaskId = taskDto.ParentTaskId,
            Status = taskDto.Status,
            DueDate = taskDto.DueDate
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto taskDto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.AssignedToId = taskDto.AssignedToId;
        task.ProjectId = taskDto.ProjectId;
        task.ParentTaskId = taskDto.ParentTaskId;
        task.Status = taskDto.Status;
        task.DueDate = taskDto.DueDate;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

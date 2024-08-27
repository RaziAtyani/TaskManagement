using System;

namespace TASK_2.DTOs
{
    // Data Transfer Object for basic task display
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }

    // Data Transfer Object for creating a new task
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }

    // Data Transfer Object for updating an existing task
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}

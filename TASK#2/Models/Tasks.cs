﻿namespace TASK_2.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Assignee { get; set; }
        public Project Project { get; set; }
    }

}

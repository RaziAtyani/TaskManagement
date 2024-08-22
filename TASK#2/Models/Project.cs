using TASK_2.models;

namespace TASK_2.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } // Name of the project
        public string Description { get; set; } // Description of the project
        public DateTime StartDate { get; set; } // Project start date
        public DateTime EndDate { get; set; } // Project end date

        // Self-referencing relationship for sub-projects
        public int? ParentProjectId { get; set; } // Nullable, for sub-projects
        public Project ParentProject { get; set; } // Navigation property for the parent project
        public ICollection<Project> SubProjects { get; set; } // Collection of sub-projects

        // Association with Registration (User)
        public int RegistrationId { get; set; } // Foreign key to the Registration (User)
        public Registration Registration { get; set; } // Navigation property to the Registration (User)
    }
}

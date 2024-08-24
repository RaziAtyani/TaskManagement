using TASK_2.models;

namespace TASK_2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RegistrationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ParentProjectId { get; set; }

        public Registration Registration { get; set; }
        public Project ParentProject { get; set; }
        public ICollection<Project> SubProjects { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}

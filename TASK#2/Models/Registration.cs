using TASK_2.models;

namespace TASK_2.Models
{
   

    public class Registration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Role Role { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}

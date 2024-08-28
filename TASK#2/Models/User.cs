using TASK_2.Models;

namespace TASK_2.models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<Tasks> AssignedTasks { get; set; }


    }
}

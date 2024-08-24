namespace TASK_2.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Team Team { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }

}

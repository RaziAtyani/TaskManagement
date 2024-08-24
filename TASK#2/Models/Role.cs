namespace TASK_2.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Registration> Registrations { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }

}

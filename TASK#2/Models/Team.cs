namespace TASK_2.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }


}

namespace TASK_2.DTOs
{
    // TeamMemberDto.cs
    public class TeamMemberDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    // CreateTeamMemberDto.cs
    public class CreateTeamMemberDto
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    // UpdateTeamMemberDto.cs
    public class UpdateTeamMemberDto
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

}

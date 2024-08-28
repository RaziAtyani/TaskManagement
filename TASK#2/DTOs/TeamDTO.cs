namespace TASK_2.DTOs
{
    // TeamDto.cs
    public class TeamDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }

    // CreateTeamDto.cs
    public class CreateTeamDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }

    // UpdateTeamDto.cs
    public class UpdateTeamDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}

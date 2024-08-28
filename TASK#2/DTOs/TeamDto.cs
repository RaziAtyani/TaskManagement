using System.ComponentModel.DataAnnotations;

namespace TASK_2.DTOs
{
    public class TeamDto
    {
        [Required(ErrorMessage = "ProjectId is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
    }

    public class TeamResponseDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}

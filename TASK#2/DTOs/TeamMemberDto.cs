using System.ComponentModel.DataAnnotations;

namespace TASK_2.DTOs
{
    public class TeamMemberDto
    {
        [Required(ErrorMessage = "TeamId is required.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }
    }

    public class TeamMemberResponseDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

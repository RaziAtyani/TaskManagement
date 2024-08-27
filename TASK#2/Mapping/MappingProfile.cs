using AutoMapper;
using TASK_2.DTOs;
using TASK_2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TASK_2.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between Team and TeamDto
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, TeamResponseDto>();

            // Mapping between TeamMember and TeamMemberDto
            CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
            CreateMap<TeamMember, TeamMemberResponseDto>();
        }
    }
}

using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASK_2.Common;
using TASK_2.DTOs;
using TASK_2.Models;
using TASK_2.Repositories;

namespace TASK_2.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IMapper _mapper;

        public TeamMemberService(ITeamMemberRepository teamMemberRepository, IMapper mapper)
        {
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<TeamMemberResponseDto>> AddTeamMemberAsync(TeamMemberDto teamMemberDto)
        {
            var teamMember = _mapper.Map<TeamMember>(teamMemberDto);
            await _teamMemberRepository.AddAsync(teamMember);
            var teamMemberResponseDto = _mapper.Map<TeamMemberResponseDto>(teamMember);
            return new OperationResult<TeamMemberResponseDto>(201, "Team member added successfully", teamMemberResponseDto);
        }

        public async Task<OperationResult<IEnumerable<TeamMemberResponseDto>>> GetAllTeamMembersAsync()
        {
            var teamMembers = await _teamMemberRepository.GetAllAsync();
            var teamMemberResponseDtos = _mapper.Map<IEnumerable<TeamMemberResponseDto>>(teamMembers);
            return new OperationResult<IEnumerable<TeamMemberResponseDto>>(200, "Team members retrieved successfully", teamMemberResponseDtos);
        }

        public async Task<OperationResult<TeamMemberResponseDto>> GetTeamMemberByIdAsync(int id)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(id);
            if (teamMember == null)
                return new OperationResult<TeamMemberResponseDto>(404, "Team member not found");

            var teamMemberResponseDto = _mapper.Map<TeamMemberResponseDto>(teamMember);
            return new OperationResult<TeamMemberResponseDto>(200, "Team member retrieved successfully", teamMemberResponseDto);
        }

        public async Task<OperationResult> UpdateTeamMemberAsync(int id, TeamMemberDto teamMemberDto)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(id);
            if (teamMember == null)
                return new OperationResult(404, "Team member not found");

            _mapper.Map(teamMemberDto, teamMember);
            await _teamMemberRepository.UpdateAsync(teamMember);
            return new OperationResult(200, "Team member updated successfully");
        }

        public async Task<OperationResult> DeleteTeamMemberAsync(int id)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(id);
            if (teamMember == null)
                return new OperationResult(404, "Team member not found");

            await _teamMemberRepository.DeleteAsync(id);
            return new OperationResult(200, "Team member deleted successfully");
        }
    }
}

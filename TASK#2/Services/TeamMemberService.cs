using TASK_2.Models;
using TASK_2.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _teamMemberRepository.GetAllTeamMembersAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        return await _teamMemberRepository.GetTeamMemberByIdAsync(id);
    }

    public async Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId)
    {
        return await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamId);
    }

    public async Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember)
    {
        return await _teamMemberRepository.AddTeamMemberAsync(teamMember);
    }

    public async Task UpdateTeamMemberAsync(TeamMember teamMember)
    {
        await _teamMemberRepository.UpdateTeamMemberAsync(teamMember);
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        await _teamMemberRepository.DeleteTeamMemberAsync(id);
    }

    public async Task<bool> TeamMemberExists(int id)
    {
        return await _teamMemberRepository.GetTeamMemberByIdAsync(id) != null;
    }
}

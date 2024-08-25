using TASK_2.Models;

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

    public async Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember)
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
}

using TASK_2.Models;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await _teamRepository.GetAllTeamsAsync();
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _teamRepository.GetTeamByIdAsync(id);
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        return await _teamRepository.AddTeamAsync(team);
    }

    public async Task UpdateTeamAsync(Team team)
    {
        await _teamRepository.UpdateTeamAsync(team);
    }

    public async Task DeleteTeamAsync(int id)
    {
        await _teamRepository.DeleteTeamAsync(id);
    }
}

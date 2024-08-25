using TASK_2.Models;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllTeamsAsync();
    Task<Team> GetTeamByIdAsync(int id);
    Task<Team> AddTeamAsync(Team team);
    Task UpdateTeamAsync(Team team);
    Task DeleteTeamAsync(int id);
}

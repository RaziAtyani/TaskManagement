using Microsoft.EntityFrameworkCore;
using TASK_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly ApplicationDbContext _context;

    public TeamMemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _context.TeamMembers
                             .Include(tm => tm.User)
                             .Include(tm => tm.Role)
                             .ToListAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        return await _context.TeamMembers
                             .Include(tm => tm.User)
                             .Include(tm => tm.Role)
                             .FirstOrDefaultAsync(tm => tm.Id == id);
    }

    public async Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId)
    {
        return await _context.TeamMembers
                             .Include(tm => tm.User)
                             .Include(tm => tm.Role)
                             .Where(tm => tm.TeamId == teamId)
                             .ToListAsync();
    }

    public async Task<TeamMember> AddTeamMemberAsync(TeamMember teamMember)
    {
        _context.TeamMembers.Add(teamMember);
        await _context.SaveChangesAsync();
        return teamMember;
    }

    public async Task UpdateTeamMemberAsync(TeamMember teamMember)
    {
        _context.Entry(teamMember).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        var teamMember = await _context.TeamMembers.FindAsync(id);
        if (teamMember != null)
        {
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
        }
    }
}

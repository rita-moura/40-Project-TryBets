using TryBets.Matches.DTO;

namespace TryBets.Matches.Repository;

public class TeamRepository : ITeamRepository
{
    protected readonly ITryBetsContext _context;
    public TeamRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<TeamDTOResponse> Get()
    {
        var teamsDTO = _context.Teams
            .Select(team => new TeamDTOResponse
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName
            }).ToList();

        return teamsDTO;
    }
}
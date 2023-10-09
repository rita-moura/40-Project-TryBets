using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        decimal decimalBet = decimal.Parse(BetValue.Replace(",", "."), CultureInfo.InvariantCulture);
        Match match = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId)!;

        if(match == null) throw new Exception("Match not found");

        if (match.MatchTeamAId != TeamId && match.MatchTeamBId != TeamId) throw new Exception("Team is not in this match");

        if (TeamId == match.MatchTeamAId) 
        {
            match.MatchTeamAValue += decimalBet;
        } else {
            match.MatchTeamBValue += decimalBet;
        }

        return match;
    }
}
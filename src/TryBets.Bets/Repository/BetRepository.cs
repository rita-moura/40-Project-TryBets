using TryBets.Bets.DTO;
using TryBets.Bets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace TryBets.Bets.Repository;

public class BetRepository : IBetRepository
{
    protected readonly ITryBetsContext _context;
    public BetRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public BetDTOResponse Post(BetDTORequest betRequest, string email)
    {
        User user = _context.Users.FirstOrDefault(u => u.Email == email)!;
        Match match = _context.Matches.FirstOrDefault(m => m.MatchId == betRequest.MatchId)!;
        Team team = _context.Teams.FirstOrDefault(t => t.TeamId == betRequest.TeamId)!;

        if (user == null) throw new Exception("User not founded");
        if (match == null) throw new Exception("Match not founded");
        if (team == null) throw new Exception("Team not founded");
        if (match.MatchFinished) throw new Exception("Match finished");
        if (match.MatchTeamAId != betRequest.TeamId && match.MatchTeamBId != betRequest.TeamId) throw new Exception("Team is not in this match");
        
        Bet newBet = new()
        {
            UserId = user.UserId,
            MatchId = match.MatchId,
            TeamId = team.TeamId,
            BetValue = betRequest.BetValue
        };

        _context.Bets.Add(newBet);
        _context.SaveChanges();

        Bet bet = _context.Bets.Include(b => b.Team).Include(b => b.Match).Where(b => b.BetId == newBet.BetId).FirstOrDefault()!;

        if (match.MatchTeamAId == betRequest.TeamId) 
        {
            match.MatchTeamAValue += betRequest.BetValue;
        } else {
            match.MatchTeamBValue += betRequest.BetValue;
        }

        _context.Matches.Update(match);
        _context.SaveChanges();

        var response = new BetDTOResponse
        {
            BetId = bet.BetId,
            MatchId = bet.MatchId,
            TeamId = bet.TeamId,
            BetValue = bet.BetValue,
            MatchDate = bet.Match!.MatchDate,
            TeamName = bet.Team!.TeamName,
            Email = bet.User!.Email
        };

        return response;
    }
    public BetDTOResponse Get(int BetId, string email)
    {
        User user = _context.Users.FirstOrDefault(u => u.Email == email)!;
        Bet bet = _context.Bets.Include(b => b.Team).Include(b => b.Match).Where(b => b.BetId == BetId).FirstOrDefault()!;

        if (user == null) throw new Exception("User not founded");
        if (bet == null) throw new Exception("Bet not founded");
        if (bet.User!.Email != email) throw new Exception("Bet view not allowed");

        var response = new BetDTOResponse
        {
            BetId = bet.BetId,
            MatchId = bet.MatchId,
            TeamId = bet.TeamId,
            BetValue = bet.BetValue,
            MatchDate = bet.Match!.MatchDate,
            TeamName = bet.Team!.TeamName,
            Email = bet.User!.Email
        };

        return response;
    }
}
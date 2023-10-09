using TryBets.Users.Models;
using TryBets.Users.DTO;

namespace TryBets.Users.Repository;

public class UserRepository : IUserRepository
{
    protected readonly ITryBetsContext _context;
    public UserRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public User Post(User user)
    {
        var verifyUser = _context.Users.Where(u => u.Email == user.Email);

        if (verifyUser.Count() > 0)
        {
            throw new Exception("E-mail already used");
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User Login(AuthDTORequest login)
    {
        var user = _context.Users.Where(u => u.Email == login.Email);

        if (user.Count() == 0 || user.First().Password != login.Password)
        {
            throw new Exception("Authentication failed");
        }

        return user.FirstOrDefault()!;
    }

}
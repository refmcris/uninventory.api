using Microsoft.EntityFrameworkCore;
using Uninventory.DBContext;
using Uninventory.DBContext.Models;
using Uninventory.Interfaces;
using Uninventory.Models.Users;

namespace Uninventory.Services
{
  public class UserService : IUserService
  {
    private readonly UninventoryDBContext _context;

    public UserService(UninventoryDBContext context)
    {
      _context = context;
    }

    public async Task<UserDTO> AddUser(UserDTO add)
    {
      var user = new User
      {
        FullName = add.FullName,
        Email = add.Email,
        UserRole = add.UserRole,
        UserPassword = add.UserPassword
      };
      await _context.User.AddAsync(user);

      await _context.SaveChangesAsync();

      return new UserDTO
      {
        UserId = user.UserId, 
        FullName = user.FullName,
        Email = user.Email,
        UserRole = user.UserRole,
        UserPassword = user.UserPassword,
        CreatedAt = user.CreatedAt
      };


    }
    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
      var users = await _context.User.ToListAsync();
      return users.Select(user => new UserDTO
      {
        UserId = user.UserId,
        FullName = user.FullName,
        Email = user.Email,
        UserRole = user.UserRole,
        UserPassword = user.UserPassword,
        CreatedAt = user.CreatedAt
      });
    }
  }
}

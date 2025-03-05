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


    private UserDTO ToUserDTO(User ur)
    {
      return new UserDTO
      {
        UserId = ur.UserId,
        FullName = ur.FullName,
        Email = ur.Email,
        UserRole = ur.UserRole,
        UserPassword = ur.UserPassword,
        CreatedAt = ur.CreatedAt
      };
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

      return ToUserDTO(user);
     


    }
    public async Task<IEnumerable<UserDTO>> GetUsers(int? UserId)
    {
      var users = await _context.User.ToListAsync();
      return users.Select(ToUserDTO).ToList();
    }

    public async Task<UserDTO> GetUser(int UserId)
    {
       var users = await GetUsers(UserId);
      if (!users.Any())
      {
        throw new Exception($"El usuario {UserId} no está registrado.");
      }
      return users.First();
    }
  }
}

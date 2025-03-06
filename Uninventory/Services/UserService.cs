using Microsoft.EntityFrameworkCore;
using Uninventory.Persistence;
using Uninventory.Interfaces;
using Uninventory.Models.Users;
using Uninventory.Persistence.Models;

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
        UserRoleId = ur.UserRole,
        UserRoleName = ur.UserRoleNavigation.Name,
        UserPassword = ur.UserPassword,
        CreatedAt = ur.CreatedAt,
        Delete = ur.Delete
      };
    }

    public async Task<UserDTO> AddUser(UserDTO add)
    {
      var user = new User
      {
        FullName = add.FullName,
        Email = add.Email,
        UserRole = add.UserRoleId,
        UserPassword = add.UserPassword
      };
      await _context.User.AddAsync(user);

      await _context.SaveChangesAsync();

      return ToUserDTO(user);
     


    }
    public async Task<IEnumerable<UserDTO>> GetUsers(int? UserId)
    {

      var query = _context.User
        .Include(u => u.UserRoleNavigation)
        .AsQueryable();

      if (UserId.HasValue)
      {
        query = query.Where(u => u.UserId == UserId.Value);
      }
      var users = await query.ToListAsync();

      return users.Select(ToUserDTO).ToList();
    }

    public async Task<User> GetUserById(int UserId)
    {
      var query = _context.User.Where(ur => ur.UserId == UserId);
      var user = await query.FirstOrDefaultAsync();

      if (user == null)
      {
        throw new Exception($"El usuario {UserId} no está registrado.");
      }
      return user;
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

    public async Task<UserDTO> SetUser(int UserId, UserDTO userDTO)
    {
      var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == UserId);


      if (user == null)
      {
        throw new Exception($"El usuario {UserId} no está registrado.");
      }
      user.FullName = userDTO.FullName ?? user.FullName;
      user.Email = userDTO.Email ?? user.Email;
      user.UserRole = userDTO.UserRoleId ?? user.UserRole;
      user.UserPassword = userDTO.UserPassword ?? user.UserPassword;


      await _context.SaveChangesAsync();
      
      return await GetUser(UserId);
    }

    public async Task<UserDTO> DeleteUser(int UserId)
    {
      var user = await GetUserById(UserId);



      user.Delete = true;


      await _context.SaveChangesAsync();

      

      return await GetUser(UserId);
    }

    public async Task<UserDTO> userLogin(string email, string password)
    {
      var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.UserPassword == password);

      if (user == null)
      {
        throw new Exception("Usuario o contraseña incorrectos.");
      }

      return ToUserDTO(user);
    }

  }
}

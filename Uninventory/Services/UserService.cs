using Microsoft.EntityFrameworkCore;
using Uninventory.Persistence;
using Uninventory.Interfaces;
using Uninventory.Models.Users;
using Uninventory.Persistence.Models;
using Uninventory.Models.Session;
using Uninventory.Common.Exceptions;

namespace Uninventory.Services
{
  public class UserService : IUserService
  {
    private readonly UninventoryDBContext _context;
    private readonly IAuthService authService;

    public UserService(UninventoryDBContext context,IAuthService authService)
    {
      this._context = context;
      this.authService = authService;
    }


    private UserDTO ToUserDTO(User ur)
    {
      return new UserDTO
      {
        UserId = ur.UserId,
        StudentCode = ur.StudentCode,
        FullName = ur.FullName,
        LastName = ur.LastName,
        Phone = ur.Phone,
        Email = ur.Email,
        UserRoleId = ur.UserRole,
        UserRoleName = ur.UserRoleNavigation.Name,
        //UserPassword = ur.UserPassword,
        CreatedAt = ur.CreatedAt,
        Delete = false
      };
    }

    public async Task<UserDTO> AddUser(UserDTO add)
    {

      bool studentCodeExists = await _context.User.AnyAsync(u => u.StudentCode == add.StudentCode);
      if (studentCodeExists)
      {
        throw new ServiceException("Ya existe un usuario con ese código de estudiante.");
      }
      bool emailExists = await _context.User.AnyAsync(u => u.Email == add.Email);
      if (emailExists)
      {
        throw new ServiceException("Ya existe un usuario con ese correo electrónico.");
      }
      bool phoneExists = await _context.User.AnyAsync(u => u.Phone == add.Phone);
      if (phoneExists)
      {
        throw new ServiceException("Ya existe un usuario con ese número de teléfono.");
      }


      var user = new User
      {
        StudentCode = add.StudentCode,
        Phone = add.Phone,
        LastName = add.LastName,
        FullName = add.FullName,
        Email = add.Email,
        UserRole = add.UserRoleId,
        UserPassword = add.UserPassword
      };
      await _context.User.AddAsync(user);

      await _context.SaveChangesAsync();

      await _context.Entry(user)
        .Reference(u => u.UserRoleNavigation)
        .LoadAsync();

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


      user.StudentCode = userDTO.StudentCode;
      user.Phone = userDTO.Phone ?? user.Phone;
      user.Email = userDTO.Email ?? user.Email;
      user.UserRole = userDTO.UserRoleId;


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

    public async Task<UserDTO> userLogin(string email, UserDTO login)
    {
      var session = await this.authService.NewSession(new NewSessionRequestDTO()
      {
        email = email,
        password = login.UserPassword,
      });
      var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

     

      return await GetUser(user.UserId);
    }

    public async Task<IEnumerable<UserDTO>> SearchUsersByName(string name)
    {
      var query = _context.User
        .Include(u => u.UserRoleNavigation)
        .Where(u => EF.Functions.Like(u.FullName.ToLower(), $"%{name.ToLower()}%"));

      var users = await query.ToListAsync();

      return users.Select(ToUserDTO).ToList();
    }


  }
}

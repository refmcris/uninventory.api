using Microsoft.EntityFrameworkCore;
using Uninventory.Controllers;
using Uninventory.Interfaces;
using Uninventory.Models.Users;
using Uninventory.Persistence;
using Uninventory.Persistence.Models;

namespace Uninventory.Services
{
  public class UserRoleService : IUserRole
  {
    private readonly UninventoryDBContext _context;

    public UserRoleService(UninventoryDBContext context)
    {
      _context = context;
    }

    public async Task<UserRoleDTO> AddRole(UserRoleDTO add)
    {
      var role = new Role
      {
        Name = add.Name
      };
      await _context.Role.AddAsync(role);

      await _context.SaveChangesAsync();

      return new UserRoleDTO
      {
        RoleId = role.RoleId,
        Name = role.Name
      }; 


    }

    public async Task<IEnumerable<UserRoleDTO>> GetRoles(int? RoleId)
    {
      var query = _context.Role.AsQueryable();

      var roles = await query.ToListAsync();
      return roles.Select(x => new UserRoleDTO
      {
        RoleId = x.RoleId,
        Name = x.Name
      });
    }



  }
}

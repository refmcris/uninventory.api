using Uninventory.Models.Users;

namespace Uninventory.Interfaces
{
  public interface IUserRole
  {
    public Task<UserRoleDTO> AddRole(UserRoleDTO add);
    public Task<IEnumerable<UserRoleDTO>> GetRoles(int? UserRole);
  }
}

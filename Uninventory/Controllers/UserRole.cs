using Microsoft.AspNetCore.Mvc;
using Uninventory.Interfaces;
using Uninventory.Models.Users;

namespace Uninventory.Controllers
{
  [ApiController]
  [Route("api/")]
  public class UserRole : ControllerBase
  {
    private readonly IUserRole _userRole;

    public UserRole(IUserRole userRole)
    {
      _userRole = userRole;
    }

    [HttpPost("userRole")]
    public async Task<ActionResult<UserRoleDTO>> AddRole(UserRoleDTO add)
    {
      return await _userRole.AddRole(add);

    }

    [HttpGet("userRole")]
    public async Task<IEnumerable<UserRoleDTO>> GetRoles(int? RoleId)
    {
      return await _userRole.GetRoles(RoleId);
    }


  }
}

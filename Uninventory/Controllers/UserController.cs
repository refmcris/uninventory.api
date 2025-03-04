using Microsoft.AspNetCore.Mvc;
using Uninventory.Interfaces;
using Uninventory.Models.Users;

namespace Uninventory.Controllers
{
  [ApiController]
  [Route("api/")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("users")]
    public async Task<UserDTO> AddUser(UserDTO add)
    {
      return await _userService.AddUser(add);

    }

    [HttpGet("users")]
    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
      return await _userService.GetUsers();
    }




  }
}

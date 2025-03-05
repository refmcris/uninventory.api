using Microsoft.AspNetCore.Mvc;
using Uninventory.Persistence;
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
    public async Task<IEnumerable<UserDTO>> GetUsers(int? UserId)
    {
      return await _userService.GetUsers(UserId);
    }

    [HttpGet("users/{UserId}")]
    public async Task<UserDTO> GetUserById(int UserId)
    {
      return await _userService.GetUser(UserId);
    }

    [HttpPut("users/{UserId}")]
    public async Task<UserDTO> SetUser(int UserId, UserDTO userDTO)
    {
      return await _userService.SetUser(UserId, userDTO);
    }

    [HttpDelete("users/{UserId}")]
    public async Task<UserDTO> DeleteUser(int UserId)
    {
      return await _userService.DeleteUser(UserId);
    }

    [HttpPost("users/login")]
    public async Task<UserDTO> userLogin(string email, string password)
    {
      return await _userService.userLogin(email, password);
    }



  }
}

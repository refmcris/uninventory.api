using Uninventory.Models.Users;

namespace Uninventory.Interfaces
{
  public interface IUserService
  {
    public Task<UserDTO> AddUser(UserDTO add);
    public Task <IEnumerable<UserDTO>> GetUsers();

  }
}

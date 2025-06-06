using Uninventory.Models.Users;

namespace Uninventory.Interfaces
{
  public interface IUserService
  {
    public Task<UserDTO> AddUser(UserDTO add);
    public Task <IEnumerable<UserDTO>> GetUsers(int? UserId);

    public Task<UserDTO> GetUser(int UserId);

    public Task<UserDTO> SetUser(int UserId, UserDTO userDTO);

    public Task<UserDTO> DeleteUser(int UserId);

    public Task<UserDTO> userLogin(string email, UserDTO add);
   
    public Task<IEnumerable<UserDTO>> SearchUsersByName(string name);
  }
}

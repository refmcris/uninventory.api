using Uninventory.Models.Session;

namespace Uninventory.Interfaces
{
  public interface IAuthService
  {
    Task<NewSessionResponseDTO> NewSession(NewSessionRequestDTO request);
  }
}

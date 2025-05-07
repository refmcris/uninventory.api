using Uninventory.Models.Sessions;

namespace Uninventory.Interfaces
{
  public interface ISessionService
  {
    public string? sessionId { get; set; }
    public string? sessionToken { get; set; }

    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int UserRole { get; set; }

    public void set(SessionDTO session);
  }
}

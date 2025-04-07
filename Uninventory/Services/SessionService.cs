using Uninventory.Interfaces;
using Uninventory.Models.Sessions;

namespace Uninventory.Services
{
  public class SessionService : ISessionService
  {
    public string? sessionId { get; set; }
    public string? sessionToken { get; set; }

    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int UserRole { get; set; }

    public void set(SessionDTO session)
    {
      UserId = session.UserId;
      UserName = session.UserName;
      sessionId = session.sessionId;
      sessionToken = session.sessionToken;
      UserRole = session.UserRole;
    }
  }
}

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Uninventory.Common.Exceptions;
using Uninventory.Controllers;
using Uninventory.Interfaces;
using Uninventory.Models.Session;
using Uninventory.Persistence;
using Uninventory.Persistence.Models;

namespace Uninventory.Services
{
  public class AuthService : IAuthService
  {
    private readonly ISessionService _sessionService;
    private readonly UninventoryDBContext _dbContext;

    public AuthService(UninventoryDBContext dbContext, ISessionService sessionService)
    {
      this._dbContext = dbContext;
      this._sessionService = sessionService;
    }

    public async Task<NewSessionResponseDTO> NewSession(NewSessionRequestDTO request)
    {
      var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == request.email);
      if (user == null)
      {
        throw new ServiceException();
      }
      if(user.UserPassword != request.password)
      {
        throw new ServiceException();
      }
      if (user.Delete)
      {
        throw new ServiceException();
      }

      var sessions = await _dbContext.Usersession.Where(us => us.UserId == us.UserId && !(us.Inactive)).ToListAsync();
      
      foreach(var session in sessions)
      {
        session.Inactive = true;
      }

      var newSession = new Usersession()
      {
        SessionId = Guid.NewGuid(),
        UserId = user.UserId,
        Token = GenerateSessionToken(),
        ExpiresOn = DateTime.Now.AddHours(24),
        Inactive = false,
        InsertBy = user.UserId,
        InsertOn = DateTime.Now
      };
      _dbContext.Usersession.Add(newSession);

      await _dbContext.SaveChangesAsync();


      var newSessionDTO = new NewSessionResponseDTO()
      {
        SessionId = newSession.SessionId.ToString(),
        SessionToken = newSession.Token
      };
      _sessionService.set(new Models.Sessions.SessionDTO()
      {
        sessionId = newSession.SessionId.ToString(),
        sessionToken = newSession.Token,
        UserId = newSession.UserId ?? user.UserId,
        UserName= user.FullName,
        UserRole=user.UserRole,
        StudentCode = user.StudentCode,
      });

      return newSessionDTO;
    }
    public static string GenerateSessionToken(int length = 32)
    {
      // Generate a secure random byte array
      using (var rng = RandomNumberGenerator.Create())
      {
        var randomBytes = new byte[length];
        rng.GetBytes(randomBytes);

        // Convert to a URL-safe Base64 string
        return Convert.ToBase64String(randomBytes)
            .Replace("+", "-")  // URL safe
            .Replace("/", "_")  // URL safe
            .TrimEnd('=');      // Remove padding
      }
    }
  }
}

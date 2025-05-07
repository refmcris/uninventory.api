using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using Uninventory.Common.Exceptions;
using Uninventory.Interfaces;
using Uninventory.Persistence;
namespace Uninventory.Middleware
{
  public class SessionMiddleware
  {
    private readonly RequestDelegate _next;

    public SessionMiddleware(RequestDelegate _next)
    {
      this._next = _next;
    }

    public async Task InvokeAsync(HttpContext context, UninventoryDBContext uninventory, ISessionService session, IUserService userService)
    {
      string? normalizedPath = context.Request.Path.Value?.TrimStart("/api/".ToCharArray()).TrimEnd('/') ?? null;
      if (normalizedPath != null && (normalizedPath.Equals("users/session") ||
        (normalizedPath.StartsWith("users/") && normalizedPath.EndsWith("login"))
        ))
      {
        context.Response.OnStarting(() =>
        {
          if (!string.IsNullOrEmpty(session.sessionId) && !string.IsNullOrEmpty(session.sessionToken))
          {
            context.Response.Headers["session-id"] = session.sessionId;
            context.Response.Headers["session-token"] = session.sessionToken;
          }
          return Task.CompletedTask;

        });
        await _next(context);

        return;
      }
      if (context.Request.Path.StartsWithSegments("/users/session") ||
      (context.Request.Path.Value?.StartsWith("/users/") == true && context.Request.Path.Value?.EndsWith("/login/") == true))
      {
        await _next(context);
        return;
      }
      if (!context.Request.Headers.TryGetValue("session-id", out StringValues value1))
      {
        throw new SessionException();
      }
      string? sessionId = value1[0];

      if (string.IsNullOrEmpty(value1[0]))
      {
        throw new SessionException();
      }
      if (!Guid.TryParse(sessionId, out Guid guid))
      {
        throw new SessionException();
      }

      var current = await uninventory.Usersession.FirstOrDefaultAsync(u => u.SessionId == guid);
      if (current == null || current.Inactive || DateTime.Now > current.ExpiresOn) {
        throw new SessionException();
      }

      var user = await uninventory.User.FirstOrDefaultAsync(u => u.UserId == current.UserId);
      if(user == null || user.Delete)
      {
        throw new SessionException();
      }

      var userdata = await userService.GetUser(user.UserId);

      session.set(new()
      {
        sessionId = sessionId,
        sessionToken = current.Token,
        UserId = current.UserId ?? user.UserId,
        UserName = user.FullName,
        UserRole = user.UserRole
      });

      await _next(context);




    }
  }
}

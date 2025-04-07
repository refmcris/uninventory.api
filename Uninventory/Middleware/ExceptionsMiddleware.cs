using Newtonsoft.Json;
using System.Net;
using Uninventory.Common.Exceptions;
using Uninventory.Interfaces;
using Uninventory.Persistence;

namespace Uninventory.Middleware
{
  public class ExceptionsMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate _next)
    {
      this._next = _next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISessionService session, UninventoryDBContext fCDbContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {

        await HandleExceptionAsync(session, httpContext, ex, fCDbContext);
      }
    }

    private async Task HandleExceptionAsync(ISessionService session, HttpContext httpContext, Exception ex, UninventoryDBContext dbContext)
    {
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
      int statusCode = (int)HttpStatusCode.InternalServerError;

      SessionException? auth = ex as SessionException;
      if (auth != null)
      {
        statusCode = (int)HttpStatusCode.Unauthorized;
        message = "Unauthorized";
      }

      AggregateException? aggregate = ex as AggregateException;

      if (aggregate != null && aggregate.InnerException != null)
      {
        ex = aggregate.InnerException;
      }

      ServiceException? custom = ex as ServiceException;

      if (custom != null && !string.IsNullOrEmpty(custom.Message))
      {
        message = custom.Message;
      }

      string logMsg = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

      if (!string.IsNullOrEmpty(message))
      {
        logMsg = message;
      }

      httpContext.Response.StatusCode = statusCode;
      message = message == string.Empty ? "Internal server error" : message;

      await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
      {
        statusCode = statusCode,
        message = message
      }));
    }
  }
}

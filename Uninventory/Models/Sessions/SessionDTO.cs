namespace Uninventory.Models.Sessions
{
  public class SessionDTO
  {
    public string? sessionId {  get; set; }
    public string? sessionToken { get; set; }

    public int StudentCode { get; set; }
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int UserRole { get; set; }

  }
}

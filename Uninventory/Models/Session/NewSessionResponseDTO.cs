namespace Uninventory.Models.Session
{
  public class NewSessionResponseDTO
  {
    public int UserId { get; set; }
    public string? SessionId { get; set; }

    public string? SessionToken { get; set; }
    public string? fullName { get; set;}

    public int userRole {  get; set; }
  }
}

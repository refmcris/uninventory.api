namespace Uninventory.Models.Users
{
  public class UserDTO
  {
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? UserRole { get; set; }
    public string? UserPassword { get; set; }
    public DateTime? CreatedAt { get; set; }

    public bool? Delete { get; set; }
  }
}

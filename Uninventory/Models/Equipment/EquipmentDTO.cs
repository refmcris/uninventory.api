namespace Uninventory.Models.Equipment
{
  public class EquipmentDTO
  {
    public int EquipmentId { get; set; }
    public string? Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Location { get; set; }
    public string? Status { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyDate { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}

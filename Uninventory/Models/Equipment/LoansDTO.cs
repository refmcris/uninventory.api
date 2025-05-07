namespace Uninventory.Models.Equipment
{
  public class LoansDTO
  {
    public int LoanId { get; set; }
    public int EquipmentId { get; set; }

    public string? EquipmentName { get; set; }
    public int UserId { get; set; }

    public string ? userName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? Status { get; set; }
  }
}

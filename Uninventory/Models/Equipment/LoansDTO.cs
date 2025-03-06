namespace Uninventory.Models.Equipment
{
  public class LoansDTO
  {
    public int LoanId { get; set; }
    public int? EquipmentId { get; set; }
    public int? UserId { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool? Status { get; set; }
  }
}

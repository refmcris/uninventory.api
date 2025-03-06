using Uninventory.Models.Equipment;

namespace Uninventory.Interfaces
{
  public interface ILoanService
  {

    Task<LoansDTO> AddLoan(LoansDTO loan);
    //Task<IEnumerable<LoansDTO>> GetLoans();
    //Task<LoansDTO> GetLoan(int id);
   
    //Task<LoansDTO> UpdateLoan(int id, LoansDTO loan);
    //Task<LoansDTO> DeleteLoan(int id);
  }
}

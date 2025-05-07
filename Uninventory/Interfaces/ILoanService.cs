using Uninventory.Models.Equipment;

namespace Uninventory.Interfaces
{
  public interface ILoanService
  {

    Task<LoansDTO> AddLoan(LoansDTO add);
    Task<LoansDTO> UpdateLoan(int id);
    Task<IEnumerable<LoansDTO>> GetLoans(int? loanId);

    Task<IEnumerable<LoansDTO>> GetLoansByUser(int? userId);

    //Task<LoansDTO> GetLoan(int id);


    //Task<LoansDTO> DeleteLoan(int id);
  }
}

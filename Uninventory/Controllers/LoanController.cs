using Microsoft.AspNetCore.Mvc;
using Uninventory.Interfaces;
using Uninventory.Models.Equipment;
using Uninventory.Persistence.Models;

namespace Uninventory.Controllers
{
  [ApiController]
  [Route("api/")]
  public class LoanController :ControllerBase
  {
    private readonly ILoanService _loanService;
    public LoanController(ILoanService loanService)
    {
      _loanService = loanService;
    }
    [HttpPost("loan")]
    public async Task<LoansDTO> AddLoan (LoansDTO add)
    {
      return await _loanService.AddLoan(add);
    }
    [HttpPut("loan/{LoanId}")]
    public async Task<LoansDTO> UpdateLoan(int LoanId)
    {
      return await _loanService.UpdateLoan(LoanId);
    }
    [HttpGet("loan")]
    public async Task<IEnumerable<LoansDTO>> GetLoans(int? loanId)
    {
      return await _loanService.GetLoans(loanId);
    }
    [HttpGet("loan/user/{userId}")]
    public async Task<IEnumerable<LoansDTO>> GetLoansByUser(int? userId)
    {
      return await _loanService.GetLoansByUser(userId);
    }
  }
}

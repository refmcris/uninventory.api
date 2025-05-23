﻿using Microsoft.EntityFrameworkCore;
using Uninventory.Interfaces;
using Uninventory.Models.Equipment;
using Uninventory.Persistence;
using Uninventory.Persistence.Models;

namespace Uninventory.Services
{
  public class LoanService : ILoanService
  {
    private readonly UninventoryDBContext _context;

    public LoanService(UninventoryDBContext context)
    {
      _context = context;
    }

    private LoansDTO ToLoanDTO(Loans lo)
    {
      return new LoansDTO
      {
        LoanId = lo.LoanId,
        EquipmentId = lo.EquipmentId,
        EquipmentName = lo.Equipment.Name,
        UserId = lo.UserId,
        userName = lo.User.FullName,
        StartDate = lo.StartDate,
        EndDate = lo.EndDate,
        Status = lo.Status
      };
    }

    public async Task<LoansDTO> AddLoan(LoansDTO add)
    {
      var loan = new Loans
      {
        EquipmentId = add.EquipmentId,
        UserId = add.UserId,
        EndDate = add.EndDate,
        Status = true


      };
      await _context.Loans.AddAsync(loan);

      await _context.SaveChangesAsync();

      return ToLoanDTO(loan);
    }
    public async Task<LoansDTO> UpdateLoan(int id)
    {
      var loan = await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == id);

      if(loan == null)
      {
        throw new Exception("Loan not found");
      }
      loan.Status = false;

      await _context.SaveChangesAsync();
      return ToLoanDTO(loan);
    }

    public async Task<IEnumerable<LoansDTO>> GetLoans(int? loanId)
    {
      var query = _context.Loans
        .Include(l => l.Equipment)
        .Include(u => u.User)
        .AsQueryable();
      if (loanId.HasValue)
      {
        query =query.Where(l=> l.LoanId == loanId.Value);
      }
      var loans = await query.ToListAsync();

      return loans.Select(ToLoanDTO).ToList();
    }

    public async Task<IEnumerable<LoansDTO>> GetLoansByUser(int? UserId) 
    { 
      var query = _context.Loans
        .Include(l => l.Equipment)
        .Include(u => u.User)
        .AsQueryable();
      if (UserId.HasValue)
      {
        query = query.Where(l => l.UserId == UserId.Value);
      }
      var loans = await query.ToListAsync();

      return loans.Select(ToLoanDTO).ToList();
    }

  }
}

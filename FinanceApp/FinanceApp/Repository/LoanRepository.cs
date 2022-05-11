using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext masterDbContext)
    {
        _context = masterDbContext;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansForUser(string userId)
    {
        return await _context.loans.Where(loan => loan.UserId == userId).ToListAsync();
    }

    public async Task<Loan> GetLoanById(int id)
    {
        return await _context.loans.FindAsync(id);
    }

    public async Task<Loan> Create(Loan loan)
    {
        loan.RemainingMonths = loan.TermInMonths;
        loan.RemainingAmount = loan.Amount;

        _context.loans.Add(loan);
        await _context.SaveChangesAsync();

        return loan;
    }

    public async Task<bool> PayNextInstallment(int id, string userId)
    {
        var loan = await _context.loans.FindAsync(id);

        if (loan == null || loan.UserId != userId) return false;

        if (loan.RemainingMonths <= 1)
        {
            loan.RemainingAmount = 0;
            loan.RemainingMonths = 0;
        }
        else
        {
            loan.RemainingAmount -= loan.LoanSchedule[0].Principal;
            loan.RemainingMonths--;
        }


        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var loanToDelete = await _context.loans.FindAsync(id);

        if (loanToDelete == null || loanToDelete.UserId != userId) return false;

        _context.loans.Remove(loanToDelete);

        await _context.SaveChangesAsync();

        return true;
    }
}
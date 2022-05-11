using FinanceApp.Models;

namespace FinanceApp.Repository;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansForUser(string userId);
    Task<Loan> GetLoanById(int id);
    Task<Loan> Create(Loan item);
    Task<bool> PayNextInstallment(int id, string userId);
    Task<bool> Delete(int id, string userId);
}
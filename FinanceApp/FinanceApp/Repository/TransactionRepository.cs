using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            _context.transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task Delete(int Id)
        {
            var transactionToDelete = await _context.transactions.FindAsync(Id);
            _context.transactions.Remove(transactionToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int Id)
        {
            return await _context.transactions.FindAsync(Id);
        }

        public async Task Update(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async  Task<IEnumerable<Transaction>> GetLastFiveTransaction(int Id)
        {
            var transactions = GetAllTransactions().Result.Where(t => t.CardId == Id).OrderBy(t => t.DateOfTransaction);
            var lastFive = transactions.Skip(Math.Max(0, transactions.Count()-5));
            return lastFive;
        }
    }
}

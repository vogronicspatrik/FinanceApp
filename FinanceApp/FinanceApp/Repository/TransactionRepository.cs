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

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.transactions.FindAsync(id);
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            if (transaction.CategoryId == 0)
            {
                transaction.CategoryId = null;
            }
            _context.transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task Update(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var transactionToDelete = await _context.transactions.FindAsync(id);
            _context.transactions.Remove(transactionToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
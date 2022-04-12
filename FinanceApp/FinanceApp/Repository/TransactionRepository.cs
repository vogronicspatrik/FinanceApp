using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.transactions.ToList();
        }
        public Transaction GetTransactionById(int Id)
        {
            return _context.transactions.FirstOrDefault(t => t.Id == Id);
        }
    }
}

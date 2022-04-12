

using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ITransactionRepository
    {
        public List<Transaction> GetAllTransactions();
        public Transaction GetTransactionById(int Id);
    }
}

﻿

using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int Id);
        Task<Transaction> Create(Transaction transaction);
        Task Update(Transaction transaction);
        Task Delete(int Id);
    }
}

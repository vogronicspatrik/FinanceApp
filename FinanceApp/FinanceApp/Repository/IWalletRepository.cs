using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetAllWalletsForUser(string userId);
        Task<Wallet> GetWalletById(int id, DateTime from, DateTime to);
        Task<Wallet> Create(Wallet wallet);
        Task<bool> Update(int id, Wallet wallet, string userId);
        Task<bool> Delete(int id, string userId);
        Task<bool> UpdateBalance(int id, int amount, string userId);
    }
}
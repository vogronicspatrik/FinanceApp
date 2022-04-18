using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IWalletRepository
    {
        Task <IEnumerable<Wallet>> GetAllWallets();
        Task <Wallet> GetWalletById(int Id);
        Task<Wallet> Create(Wallet wallet);
        Task Update(Wallet wallet);
        Task Delete(int Id);
    }
}

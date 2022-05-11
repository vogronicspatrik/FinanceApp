using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocks();
        Task<IEnumerable<Stock>> GetAllStocksForUser(string userId);
        Task<Stock> GetStockById(int Id);
        Task<Stock> Create(Stock stock);
        Task Update(Stock stock, string userId);
        Task Delete(int Id, string userId);
    }
}

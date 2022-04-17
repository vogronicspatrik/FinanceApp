using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocks();
        Task<Stock> GetStockById(int Id);
        Task<Stock> Create(Stock stock);
        Task Update(Stock stock);
        Task Delete(int Id);
    }
}

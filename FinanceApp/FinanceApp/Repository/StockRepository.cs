using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<Stock> Create(Stock stock)
        {
            _context.stocks.Add(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task Delete(int Id, string userId)
        {
            var stockToDelete = await _context.stocks.FindAsync(Id);
            _context.stocks.Remove(stockToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            return await _context.stocks.ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllStocksForUser(string userId)
        {
            return await _context.stocks
            .Where(stock => stock.UserId == userId)
            .ToListAsync();
        }

        public async Task<Stock> GetStockById(int Id)
        {
            return await _context.stocks.FindAsync(Id);
        }

        public async Task Update(Stock stock, string userId)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

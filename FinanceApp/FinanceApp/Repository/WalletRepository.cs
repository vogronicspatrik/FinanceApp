using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<Wallet> Create(Wallet wallet)
        {
            _context.wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        public async Task Delete(int Id)
        {
            var walletToDelete = await _context.wallets.FindAsync(Id);
            _context.wallets.Remove(walletToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return await _context.wallets.ToListAsync();
        }

        public async Task<Wallet> GetWalletById(int Id)
        {
            return await _context.wallets.FindAsync(Id);
        }

        public async Task Update(Wallet wallet)
        {
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

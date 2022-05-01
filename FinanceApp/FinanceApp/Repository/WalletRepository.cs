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

        public async Task<IEnumerable<Wallet>> GetAllWalletsForUser(string userId)
        {
            return await _context.wallets
                .Where(wallet => wallet.UserId == userId)
                .Include(wallet =>
                    wallet.Transactions.OrderByDescending(transaction => transaction.DateOfTransaction).Take(5))
                .ThenInclude(transaction => transaction.Category)
                .ToListAsync();
        }

        public async Task<Wallet> GetWalletById(int id, DateTime from, DateTime to)
        {
            return await _context.wallets.Where(wallet => wallet.Id == id)
                .Include(wallet => wallet.Transactions
                    .Where(transaction => transaction.DateOfTransaction >= from && transaction.DateOfTransaction <= to)
                    .OrderBy(transaction => transaction.DateOfTransaction)
                )
                .ThenInclude(transaction => transaction.Category)
                .FirstAsync();
        }

        public async Task<Wallet> Create(Wallet wallet)
        {
            _context.wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        public async Task<bool> Update(int id, Wallet wallet, string userId)
        {
            var walletToUpdate = await _context.wallets.FindAsync(id);

            if (walletToUpdate == null || walletToUpdate.UserId != userId) return false;

            walletToUpdate.Location = wallet.Location;
            walletToUpdate.Type = wallet.Type;
            walletToUpdate.Owner = wallet.Owner;
            walletToUpdate.AccountNumber = wallet.AccountNumber;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var walletToDelete = await _context.wallets.FindAsync(id);

            if (walletToDelete == null || walletToDelete.UserId != userId) return false;

            _context.wallets.Remove(walletToDelete);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBalance(int id, int amount, string userId)
        {
            var walletToUpdate = await _context.wallets.FindAsync(id);

            if (walletToUpdate == null || walletToUpdate.UserId != userId) return false;

            walletToUpdate.Balance += amount;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
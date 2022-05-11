using AutoMapper.Internal;
using FinanceApp.Controllers;
using FinanceApp.Data;
using FinanceApp.Enums;
using FinanceApp.Models;
using FinanceApp.Models.StatisticModels;
using FinanceApp.Other;
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

        public async Task<List<CategoryStatistic>[]> GetCategoryStatistics(string userId, Currency currency,
            DateTime from, DateTime to)
        {
            var outputIncome = new List<CategoryStatistic>()
            {
                new CategoryStatistic()
                {
                    CategoryId = 0,
                    CategoryName = "Uncategorized",
                    CategoryColor = "#F0F0F0"
                }
            };
            var incomeTotal = 0.0;
            var outputExpense = new List<CategoryStatistic>()
            {
                new CategoryStatistic()
                {
                    CategoryId = 0,
                    CategoryName = "Uncategorized",
                    CategoryColor = "#F0F0F0"
                }
            };
            var expenseTotal = 0.0;

            var walletIds = await _context.wallets.Where(wallet => wallet.UserId == userId).Select(wallet => wallet.Id)
                .ToListAsync();

            var transactions = await _context.transactions
                .Include(t => t.Wallet)
                .Where(t => t.Wallet.UserId == userId && t.DateOfTransaction >= from && t.DateOfTransaction <= to)
                .ToListAsync();

            var categories = await _context.categories.Where(category => category.UserId == userId).ToListAsync();

            foreach (var category in categories)
            {
                outputIncome.Add(new CategoryStatistic()
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    CategoryColor = category.Color
                });
                outputExpense.Add(new CategoryStatistic()
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    CategoryColor = category.Color
                });
            }

            var exchangeRate =
                (await HelperMethods.FxRealTime(
                    currency == Currency.EUR ? Currency.HUF.ToString() : Currency.EUR.ToString(), currency.ToString()))
                .ExchangeRate;

            foreach (var t in transactions)
            {
                var exchangeValue = t.Wallet.Currency == currency ? t.Price : t.Price * exchangeRate;
                if (t.Type == TransactionType.INCOME)
                {
                    if (t.CategoryId == null)
                    {
                        outputIncome[0].Value += exchangeValue;
                        incomeTotal += exchangeValue;
                        continue;
                    }

                    for (var i = 0; i < outputIncome.Count; i++)
                    {
                        if (outputIncome[i].CategoryId != t.CategoryId) continue;
                        outputIncome[i].Value += exchangeValue;
                        incomeTotal += exchangeValue;
                        break;
                    }
                }
                else
                {
                    if (t.CategoryId == null)
                    {
                        outputExpense[0].Value += exchangeValue;
                        expenseTotal += exchangeValue;
                        continue;
                    }

                    for (var i = 0; i < outputExpense.Count; i++)
                    {
                        if (outputExpense[i].CategoryId != t.CategoryId) continue;
                        outputExpense[i].Value += exchangeValue;
                        expenseTotal += exchangeValue;
                        break;
                    }
                }
            }

            outputIncome = outputIncome.Where(c => c.Value != 0).OrderByDescending(c => c.Value).ToList();
            outputExpense = outputExpense.Where(c => c.Value != 0).OrderByDescending(c => c.Value).ToList();

            outputIncome.ForAll(c => c.Percentage = c.Value / incomeTotal);
            outputExpense.ForAll(c => c.Percentage = c.Value / expenseTotal);

            return new[] {outputIncome, outputExpense};
        }

        public async Task<IEnumerable<object>> GetSummary(string userId)
        {
            var output = await _context.wallets
                .Where(wallet => wallet.UserId == userId)
                .GroupBy(wallet => new {currency = wallet.Currency})
                .Select(group => new
                {
                    group.Key.currency,
                    summary = group.Sum(wallet => wallet.Balance)
                })
                .ToListAsync();

            return output;
        }
    }
}
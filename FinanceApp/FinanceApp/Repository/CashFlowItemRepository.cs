using FinanceApp.Data;
using FinanceApp.Enums;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository;

public class CashFlowItemRepository : ICashFlowItemRepository
{
    private readonly ApplicationDbContext _context;

    public CashFlowItemRepository(ApplicationDbContext masterDbContext)
    {
        _context = masterDbContext;
    }

    public async Task<IEnumerable<CashFlowItem>> GetAllCashFlowItemsForUser(string userId)
    {
        return await _context.cashFlowItems.Where(item => item.UserId == userId).OrderBy(item => item.Type)
            .ThenBy(item => item.RecurrencyPeriod).ToListAsync();
    }

    public async Task<CashFlowItem> GetCashFlowItemById(int id)
    {
        return await _context.cashFlowItems.FindAsync(id);
    }

    public async Task<CashFlowItem> Create(CashFlowItem item)
    {
        _context.cashFlowItems.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var itemToDelete = await _context.cashFlowItems.FindAsync(id);

        if (itemToDelete == null || itemToDelete.UserId != userId) return false;

        _context.cashFlowItems.Remove(itemToDelete);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<object>> GetSummary(string userId)
    {
        var output = await _context.cashFlowItems
            .Where(item => item.UserId == userId)
            .GroupBy(item => new {currency = item.Currency, period = item.RecurrencyPeriod})
            .Select(group => new
            {
                group.Key.currency,
                group.Key.period,
                incomeSummary = group.Where(item => item.Type == CashFlowDirectionType.INCOME).Sum(item => item.Amount),
                expenseSummary = group.Where(item => item.Type == CashFlowDirectionType.COST).Sum(item => item.Amount)
            })
            .OrderBy(g => g.currency)
            .ThenBy(g => g.period)
            .ToListAsync();

        return output;
    }
}
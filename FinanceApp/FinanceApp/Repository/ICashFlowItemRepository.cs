using FinanceApp.Models;

namespace FinanceApp.Repository;

public interface ICashFlowItemRepository
{
    Task<IEnumerable<CashFlowItem>> GetAllCashFlowItemsForUser(string userId);
    Task<CashFlowItem> GetCashFlowItemById(int id);
    Task<CashFlowItem> Create(CashFlowItem item);
    Task<bool> Delete(int id, string userId);
    Task<IEnumerable<object>> GetSummary(string userId);
}
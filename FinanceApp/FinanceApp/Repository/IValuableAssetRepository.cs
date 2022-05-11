using FinanceApp.Models;

namespace FinanceApp.Repository;

public interface IValuableAssetRepository
{
    Task<IEnumerable<ValuableAsset>> GetAllAssetsForUser(string userId);
    Task<ValuableAsset> GetAssetById(int id);
    Task<ValuableAsset> Create(ValuableAsset asset);
    Task<bool> Delete(int id, string userId);
    Task<IEnumerable<object>> GetSummary(string userId);
}
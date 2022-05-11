using System.Linq;
using FinanceApp.Data;
using FinanceApp.Models;
using FinanceApp.Models.StatisticModels;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository;

public class ValuableAssetRepository : IValuableAssetRepository
{
    private readonly ApplicationDbContext _context;

    public ValuableAssetRepository(ApplicationDbContext masterDbContext)
    {
        _context = masterDbContext;
    }

    public async Task<IEnumerable<ValuableAsset>> GetAllAssetsForUser(string userId)
    {
        return await _context.valuableAssets.Where(asset => asset.UserId == userId).ToListAsync();
    }

    public async Task<ValuableAsset> GetAssetById(int id)
    {
        return await _context.valuableAssets.FindAsync(id);
    }

    public async Task<ValuableAsset> Create(ValuableAsset asset)
    {
        _context.valuableAssets.Add(asset);
        await _context.SaveChangesAsync();

        return asset;
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var assetToDelete = await _context.valuableAssets.FindAsync(id);

        if (assetToDelete == null || assetToDelete.UserId != userId) return false;

        _context.valuableAssets.Remove(assetToDelete);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<object>> GetSummary(string userId)
    {
        var valuableAssets = await _context.valuableAssets
            .Where(asset => asset.UserId == userId)
            .ToListAsync();

        var output = new List<ValuableAssetSummary>();

        foreach (var item in valuableAssets)
        {
            var found = false;
            foreach (var currencySummary in output.Where(currencySummary => currencySummary.Currency == item.Currency))
            {
                currencySummary.WithAmortization += item.CurrentValue;
                currencySummary.WithoutAmortization += item.OriginalValue;
                found = true;
            }

            if (!found)
            {
                output.Add(new ValuableAssetSummary()
                {
                    Currency = item.Currency, WithAmortization = item.CurrentValue,
                    WithoutAmortization = item.OriginalValue
                });
            }
        }

        return output;
    }
}
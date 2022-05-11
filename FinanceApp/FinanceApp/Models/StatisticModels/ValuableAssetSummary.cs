using FinanceApp.Enums;

namespace FinanceApp.Models.StatisticModels;

public class ValuableAssetSummary
{
    public Currency Currency { get; set; }
    
    public int WithAmortization { get; set; }
    
    public int WithoutAmortization { get; set; }
}
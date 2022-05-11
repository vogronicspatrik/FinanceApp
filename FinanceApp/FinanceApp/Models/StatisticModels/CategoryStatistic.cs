namespace FinanceApp.Models.StatisticModels;

public class CategoryStatistic
{
    public int CategoryId { get; set; }
    
    public string CategoryName { get; set; }
    
    public string CategoryColor { get; set; }
    
    public double Percentage { get; set; }
    
    public double Value { get; set; }
}
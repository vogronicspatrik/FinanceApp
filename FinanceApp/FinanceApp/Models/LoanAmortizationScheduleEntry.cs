namespace FinanceApp.Models;

public class LoanAmortizationScheduleEntry
{
    public LoanAmortizationScheduleEntry(int index, double startingBalance, double interest, double principal, double endingBalance)
    {
        Index = index;
        StartingBalance = startingBalance;
        Interest = interest;
        Principal = principal;
        EndingBalance = endingBalance;
    }

    public int Index { get; set; }
    
    public double StartingBalance { get; set; }
    
    public double Interest { get; set; }
    
    public double Principal { get; set; }
    
    public double EndingBalance { get; set; }
}
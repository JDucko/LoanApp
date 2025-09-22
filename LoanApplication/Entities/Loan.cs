
namespace LoanApplication.Entities;

public enum PayFrequency
{
    
    Monthly = 1,
    BiMonthly = 2,
    SemiAnnually = 6,
    Annually = 12
}
public class Loan : IEntity<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string LoanName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public int LoanTermInMonths { get; set; }
    public PayFrequency Frequency { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<LoanSchedule>? LoanSchedules { get; internal set; }
}
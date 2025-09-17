namespace LoanApplication.Models;

public enum PayFrequency
{
    BiMonthly,
    Monthly,
    SemiAnnually,
    Annually
}
public class Loan : IEntity<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public PayFrequency Frequency { get; set; }
    public DateTime CreatedAt { get; set; }
}
namespace LoanApplication.Models;

public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public int PayFrequency { get; set; }
}
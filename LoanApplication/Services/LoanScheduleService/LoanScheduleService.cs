
using LoanApplication.Entities;
using LoanApplication.Repos;

namespace LoanApplication.Services;

public class LoanScheduleService : ILoanScheduleService
{
    private readonly ILoanScheduleRepository _loanScheduleRepository;

    public LoanScheduleService(ILoanScheduleRepository loanScheduleRepository)
    {
        _loanScheduleRepository = loanScheduleRepository;
    }

    public async Task<IEnumerable<LoanSchedule>> GetScheduleByLoanId(int loanId)
    {
        return await _loanScheduleRepository.GetLoanScheduleByLoanId(loanId);
    }

    public Loan CreateSchedule(Loan loan)
    {
        var loanSchedules = new List<LoanSchedule>();

        // Calculate the number of payments
        var totalPayments = loan.LoanTermInMonths / (int)loan.Frequency;
        if (totalPayments <= 0) totalPayments = 1;

        // Changing to use period rate based on frequency
        var annualRate = loan.AnnualInterestRate / 100m;
        var periodMonths = (int)loan.Frequency;
        var periodRate = annualRate * (periodMonths / 12m);

        // Using amortization formula A = P * r / (1 - (1+r)^-n)
        decimal payment;

        // Check if interest rate is zero
        // Else apply amortization formula
        if (periodRate == 0m)
        {
            payment = Math.Round(loan.Amount / totalPayments, 2);
        }
        else
        {
            // (1 + r)^-n
            var denom = 1m - (decimal)Math.Pow((double)(1m + periodRate), -totalPayments);
            if (denom == 0m)
                payment = Math.Round(loan.Amount / totalPayments, 2);
            else
                payment = Math.Round(loan.Amount * periodRate / denom, 2);
        }

        var balance = loan.Amount;

        // Generate LoanSchedule entries
        for (int i = 1; i <= totalPayments; i++)
        {
            // interest for this period
            var interest = Math.Round(balance * periodRate, 2);

            // Last payment should have the remaining principal + interest.
            if (i == totalPayments)
            {
                var finalPayment = Math.Round(balance + interest, 2);
                var schedule = new LoanSchedule
                {
                    LoanId = loan.Id,
                    Month = i,
                    MonthlyPayment = finalPayment,
                    RemainingBalance = 0m
                };
                loanSchedules.Add(schedule);
                balance = 0m;
            }
            else
            {
                var principalPayment = payment - interest;
                if (principalPayment < 0m)
                    principalPayment = 0m;
                var newBalance = Math.Round(balance - principalPayment, 2);

                var schedule = new LoanSchedule
                {
                    LoanId = loan.Id,
                    Month = i,
                    MonthlyPayment = payment,
                    RemainingBalance = newBalance
                };
                loanSchedules.Add(schedule);
                balance = newBalance;
            }
        }
        loan.LoanSchedules = loanSchedules;

        return loan;
    }
}
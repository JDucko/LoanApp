
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

        // Calculate the number of payments & montlhy payment
        var totalPayments = loan.LoanTermInMonths / (int)loan.Frequency;
        var monthlyPayment = Math.Round(loan.Amount / totalPayments, 2);
        var convertedInterestRate = loan.AnnualInterestRate / 100;
        var balance = loan.Amount;
        var monthlyInterest = convertedInterestRate / 12;

        // Generate loan schedules
        for (int i = 1; i <= totalPayments; i++)
        {
            var schedule = new LoanSchedule
            {
                LoanId = loan.Id,
                Month = i,
                MonthlyPayment = monthlyPayment,
                RemainingBalance = Math.Round(balance + (balance * monthlyInterest) - monthlyPayment, 2)
            };
            balance -= monthlyPayment;
            loanSchedules.Add(schedule); ;
        }
        loan.LoanSchedules = loanSchedules;

        return loan;
    }
}
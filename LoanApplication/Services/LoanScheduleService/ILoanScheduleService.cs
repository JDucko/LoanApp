
using LoanApplication.Entities;

namespace LoanApplication.Services;

public interface ILoanScheduleService
{
    Task<IEnumerable<LoanSchedule>> GetScheduleByLoanId(int loanId);
    Loan CreateSchedule(Loan loan);
}
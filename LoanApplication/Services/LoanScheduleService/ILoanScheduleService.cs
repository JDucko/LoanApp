
using LoanApplication.Entities;

namespace LoanApplication.Services;

public interface ILoanScheduleService
{
    Task<LoanSchedule?> GetScheduleByLoanId(int loanId);
    Task<LoanSchedule> CreateScheduleAsync(LoanSchedule schedule);
}

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

    public async Task<LoanSchedule?> GetScheduleByLoanId(int loanId)
    {
        return await _loanScheduleRepository.GetByIdAsync(loanId);
    }

    public async Task<LoanSchedule> CreateScheduleAsync(LoanSchedule schedule)
    {
        return await _loanScheduleRepository.AddAsync(schedule);
    }
}
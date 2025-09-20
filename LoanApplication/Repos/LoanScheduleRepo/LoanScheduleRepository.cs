
using LoanApplication.Entities;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos.LoanScehduleRepo;

public class LoanScheduleRepository : RepoBase<LoanSchedule, int>, ILoanScheduleRepository
{
    public LoanScheduleRepository(Context context) : base(context)
    {
    }

    public async Task<IEnumerable<LoanSchedule>> GetLoanScheduleByLoanId(int loanId)
    {
        return await  Task.Run(() =>  _context.Set<LoanSchedule>().Where(schedule => schedule.LoanId == loanId));
    }
}

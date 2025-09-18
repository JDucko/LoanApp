
using LoanApplication.Entities;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos.LoanScehduleRepo;

public class LoanScheduleRepository : RepoBase<LoanSchedule, int>, ILoanScheduleRepository
{
    public LoanScheduleRepository(Context context) : base(context)
    {
    }
}

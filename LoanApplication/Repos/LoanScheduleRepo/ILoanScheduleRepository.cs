using LoanApplication.Entities;

namespace LoanApplication.Repos
{
    public interface ILoanScheduleRepository : LoanApplication.Repos.Base.IRepoBase<LoanSchedule, int>
    {
        // Add LoanSchedule-specific methods here if needed
        
        Task<IEnumerable<LoanSchedule>> GetLoanScheduleByLoanId(int userId);
    }
}
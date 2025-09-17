using LoanApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanApplication.Repos
{
    public interface ILoanRepository : LoanApplication.Repos.Base.IRepoBase<Loan, int>
    {
        // Add Loan-specific methods here if needed
        IEnumerable<Loan> GetLoansByUserId(int userId);
    }
}

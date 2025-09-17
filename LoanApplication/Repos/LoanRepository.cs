
using Microsoft.EntityFrameworkCore;
using LoanApplication.Models;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos;

public class LoanRepository : RepoBase<Loan, int>, ILoanRepository
{
    public LoanRepository(Context context) : base(context)
    {
    }

    public IEnumerable<T> GetLoansByUserId(long userId)
    {
        return _context.Set<Loan>().Where(loan => loan.UserId == userId);
    }
}
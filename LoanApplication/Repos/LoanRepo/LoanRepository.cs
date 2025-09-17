
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LoanApplication.Models;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos.LoanRepo;

public class LoanRepository : RepoBase<Loan, int>, ILoanRepository
{
    public LoanRepository(Context context) : base(context)
    {
    }

    public IEnumerable<Loan> GetLoansByUserId(int userId)
    {
        return _context.Set<Loan>().Where(loan => loan.UserId == userId);
    }
}
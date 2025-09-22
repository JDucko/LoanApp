
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LoanApplication.Entities;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos.LoanRepo;

public class LoanRepository : RepoBase<Loan, int>, ILoanRepository
{
    public LoanRepository(Context context) : base(context)
    {
    }

    public async Task<IEnumerable<Loan>> GetLoansByUserId(int userId)
    {
        return await Task.Run(() =>  _context.Set<Loan>().Where(loan => loan.UserId == userId));
    }
}
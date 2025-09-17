
using LoanApplication.Models;

namespace LoanApplication.Services;

public interface ILoanService
{
    Task<Loan?> GetLoanByIdAsync(int loanId);
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<IEnumerable<Loan>> GetAllLoansByUserIdAsync(long);
    Task<Loan> CreateLoanAsync(Loan loan);
}
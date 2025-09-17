
using LoanApplication.Models;

namespace LoanApplication.Services;

public interface ILoanService
{
    Task<Loan?> GetLoanByIdAsync(int loanId);
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> CreateLoanAsync(Loan loan);
    Task<bool> UpdateLoanAsync(Loan loan);
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Entities;
using LoanApplication.Repos;

namespace LoanApplication.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _loanRepository.GetAllAsync();
    }

    public async Task<Loan?> GetLoanByIdAsync(int id)
    {
        return await _loanRepository.GetByIdAsync(id);
    }
    
    public async Task<IEnumerable<Loan>> GetAllLoansByUserIdAsync(int userId)
    {
        // Implement logic to get loans by user ID if needed
        return await Task.FromResult(_loanRepository.GetLoansByUserId(userId));
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        loan.CreatedAt = DateTime.UtcNow;
        return await _loanRepository.AddAsync(loan);
    }
    // Implement other methods as needed
}
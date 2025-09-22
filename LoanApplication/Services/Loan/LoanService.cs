using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Entities;
using LoanApplication.Repos;
using Microsoft.Extensions.Logging;

namespace LoanApplication.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ILogger<LoanService> _logger;

    public LoanService(ILoanRepository loanRepository, ILogger<LoanService> logger)
    {
        _loanRepository = loanRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        
        return loans;
    }

    public async Task<Loan?> GetLoanByIdAsync(int id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);
        if (loan == null)
        {
            _logger.LogWarning("Loan with ID {LoanId} not found", id);
        }
        
        return loan;
    }
    
    public async Task<IEnumerable<Loan>> GetAllLoansByUserIdAsync(int userId)
    {
        var loans = await _loanRepository.GetLoansByUserId(userId);
        
        return loans;
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        try
        {
            loan.CreatedAt = DateTime.UtcNow;
            var created = await _loanRepository.AddAsync(loan);
            
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating loan for user {UserId}", loan.UserId);
            throw;
        }
    }
    // Implement other methods as needed
}
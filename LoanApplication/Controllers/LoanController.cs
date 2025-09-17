using Microsoft.AspNetCore.Mvc;
using LoanApplication.Models;
using LoanApplication.Services;

namespace LoanApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    // GET: api/Loan
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoan()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    // GET: api/Loan/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Loan>> GetLoanById(int id)
    {
        var loan = await _loanService.GetLoanByIdAsync(id);

        if (loan == null)
        {
            return NotFound();
        }

        return Ok(loan);
    }

    // POST: api/Loan
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        var created = await _loanService.CreateLoanAsync(loan);
        return CreatedAtAction(nameof(GetLoanById), new { id = created.Id }, created);
    }

    private Task<bool> LoanExistsAsync(int id)
    {
        // Delegate to service/repository if needed; keep as placeholder for now
        return Task.FromResult(false);
    }
}

using Microsoft.AspNetCore.Mvc;
using LoanApplication.Entities;
using LoanApplication.Services;

namespace LoanApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly ILoanScheduleService _loanScheduleService;

    public LoanController(ILoanService loanService, ILoanScheduleService loanScheduleService)
    {
        _loanService = loanService;
        _loanScheduleService = loanScheduleService;
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
        //TODO: Validation and error handling
        if (loan == null)
        {
            return BadRequest("Loan object is null.");
        }

        if (loan.Amount <= 0)
        {
            return BadRequest("Loan amount must be greater than zero.");
        }

        if (loan.AnnualInterestRate < 0)
        {
            return BadRequest("Interest rate cannot be negative.");
        }
        
        if (!Enum.IsDefined(typeof(PayFrequency), loan.Frequency))
        {
            return BadRequest("Invalid pay frequency.");
        }

        var createdLoan = await _loanService.CreateLoanAsync(loan);
        return CreatedAtAction(nameof(GetLoanById), new { id = createdLoan.Id }, createdLoan);
    }

    private Task<bool> LoanExistsAsync(int id)
    {
        // Delegate to service/repository if needed; keep as placeholder for now
        return Task.FromResult(false);
    }
}

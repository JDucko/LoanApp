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
    private readonly IUserService _userService;

    public LoanController(ILoanService loanService, ILoanScheduleService loanScheduleService, IUserService userService)
    {
        _loanService = loanService;
        _loanScheduleService = loanScheduleService;
        _userService = userService;
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

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoansByUserId(int userId)
    {
        var loans = await _loanService.GetAllLoansByUserIdAsync(userId);

        if (loans == null || !loans.Any())
        {
            return Ok(new List<Loan>()); // Return empty list if no loans found
        }

        return Ok(loans);
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

        if (loan.LoanName == null || loan.LoanName.Trim() == string.Empty)
        {
            return BadRequest("Loan name is required.");
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

        var user = await _userService.GetUserAsync(loan.UserId);
        if (user == null)
        {
            return BadRequest("UserId does not exist.");
        }

        // Create Loan Schedules
        loan = _loanScheduleService.CreateSchedule(loan);

        var createdLoan = await _loanService.CreateLoanAsync(loan);
        return CreatedAtAction(nameof(GetLoanById), new { id = createdLoan.Id }, createdLoan);
    }

    private Task<bool> LoanExistsAsync(int id)
    {
        // Delegate to service/repository if needed; keep as placeholder for now
        return Task.FromResult(false);
    }
}

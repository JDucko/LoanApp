using Microsoft.AspNetCore.Mvc;
using LoanApplication.Services;
using LoanApplication.Entities;

namespace LoanApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanScheduleController : ControllerBase
    {
        private readonly ILoanScheduleService _loanScheduleService;

        public LoanScheduleController(ILoanScheduleService loanScheduleService)
        {
            _loanScheduleService = loanScheduleService;
        }

        [HttpGet("{loanId}")]
        public async Task<ActionResult<IEnumerable<LoanSchedule>>> GetSchedule(int loanId)
        {
            var schedule = await _loanScheduleService.GetScheduleByLoanId(loanId);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }
    }
}
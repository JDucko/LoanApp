using Microsoft.AspNetCore.Mvc;
using LoanApplication.Services;

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
        public IActionResult GetSchedule(int loanId)
        {
            var schedule = _loanScheduleService.GetScheduleByLoanId(loanId);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }
    }
}
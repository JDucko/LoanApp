using System;

namespace LoanApplication.Entities
{
    public class LoanSchedule : IEntity<int>
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int Month { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
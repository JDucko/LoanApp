using Microsoft.EntityFrameworkCore;
using LoanApplication.Entities;

namespace LoanApplication.Data;

public class Context : DbContext
 {
     public Context(DbContextOptions<Context> options)
         : base(options)
     {
     }
 
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Loan> Loans { get; set; } = default!;
    public DbSet<LoanSchedule> LoanSchedules { get; set; } = default!;
 }
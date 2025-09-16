using Microsoft.EntityFrameworkCore;
using LoanApplication.Models;
 
 namespace LoanApplication.Models;
 
 public class LoanContext : DbContext
 {
     public LoanContext(DbContextOptions<LoanContext> options)
         : base(options)
     {
     }
 
     public DbSet<User> Users { get; set; } = null!;
 
public DbSet<LoanApplication.Models.Loan> Loan { get; set; } = default!;
 }
using Microsoft.EntityFrameworkCore;
 
 namespace LoanApplication.Models;
 
 public class LoanContext : DbContext
 {
     public LoanContext(DbContextOptions<LoanContext> options)
         : base(options)
     {
     }
 
     public DbSet<User> Users { get; set; } = null!;
 }
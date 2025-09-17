using LoanApplication.Models;
using LoanApplication.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("LoanList"));
// Repository and service registrations
// Register open-generic repository base so concrete repos can be resolved if needed
builder.Services.AddScoped(typeof(LoanApplication.Repos.Base.IRepoBase<,>), typeof(LoanApplication.Repos.Base.RepoBase<,>));
// Concrete registrations
builder.Services.AddScoped<LoanApplication.Repos.ILoanRepository, LoanApplication.Repos.LoanRepository>();
builder.Services.AddScoped<LoanApplication.Services.ILoanService, LoanApplication.Services.LoanService>();
// UnitOfWork registration removed

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

using LoanApplication.Entities;
using LoanApplication.Data;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using NSwag.AspNetCore.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog;
using Serilog.Events;

// Use the standard Autofac service provider factory

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog from appsettings (builder.Configuration)
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        // Replace default logging with Serilog
        builder.Host.UseSerilog();

        // Use Autofac as the DI container
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // Load Autofac modules with registrations
            containerBuilder.RegisterModule(new LoanApplication.Repos.RepositoriesModule());
            containerBuilder.RegisterModule(new LoanApplication.Services.ServicesModule());
        });

        // Configure OpenAPI via NSwag for generation and Swashbuckle for UI
        builder.Services.AddOpenApiDocument();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("LoanList"));

        try
        {
            Log.Information("Starting web host");

            var app = builder.Build();

            // enable request logging
            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Serve static files from wwwroot for the basic frontend UI
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

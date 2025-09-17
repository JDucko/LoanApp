// Use the standard Autofac service provider factory

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

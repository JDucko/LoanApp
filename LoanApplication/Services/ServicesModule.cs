using Autofac;

namespace LoanApplication.Services;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LoanApplication.Services.LoanService>()
            .As<LoanApplication.Services.ILoanService>()
            .InstancePerLifetimeScope();
    }
}

using Autofac;

namespace LoanApplication.Services;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LoanService>()
            .As<ILoanService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserService>()
            .As<IUserService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<LoanScheduleService>()
            .As<ILoanScheduleService>()
            .InstancePerLifetimeScope();  
    }
}

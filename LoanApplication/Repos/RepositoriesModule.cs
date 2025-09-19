using Autofac;

namespace LoanApplication.Repos;

public class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // register open-generic repository base
        builder.RegisterGeneric(typeof(Repos.Base.RepoBase<,>))
            .As(typeof(Repos.Base.IRepoBase<,>))
            .InstancePerLifetimeScope();

        // Concrete repository registrations
        builder.RegisterType<Repos.LoanRepo.LoanRepository>()
            .As<Repos.ILoanRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<Repos.UserRepo.UserRepository>()
            .As<Repos.UserRepo.IUserRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<Repos.LoanScehduleRepo.LoanScheduleRepository>()
            .As<Repos.ILoanScheduleRepository>()
            .InstancePerLifetimeScope();
    }
}

using Autofac;

namespace LoanApplication.Repos;

public class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // register open-generic repository base
        builder.RegisterGeneric(typeof(LoanApplication.Repos.Base.RepoBase<,>))
            .As(typeof(LoanApplication.Repos.Base.IRepoBase<,>))
            .InstancePerLifetimeScope();

        // Concrete repository registrations
        builder.RegisterType<LoanApplication.Repos.LoanRepository>()
            .As<LoanApplication.Repos.ILoanRepository>()
            .InstancePerLifetimeScope();
    }
}

namespace LoanApplication.Repos.UserRepo
{
    public class UserRepository : Repos.Base.RepoBase<User>, IUserRepository
    {
        public UserRepository(LoanDbContext context) : base(context)
        {
        }

        // Implement User-specific methods here if needed
    }
}
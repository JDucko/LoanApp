
using LoanApplication.Entities;

namespace LoanApplication.Repos.UserRepo
{
    public class UserRepository : LoanApplication.Repos.Base.RepoBase<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        // Implement User-specific methods here if needed
    }
}
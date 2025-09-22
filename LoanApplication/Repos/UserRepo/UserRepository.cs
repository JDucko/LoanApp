
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LoanApplication.Entities;
using LoanApplication.Data;
using LoanApplication.Repos.Base;

namespace LoanApplication.Repos.UserRepo
{
    public class UserRepository : RepoBase<User, int>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        // Implement User-specific methods here if needed
    }
}
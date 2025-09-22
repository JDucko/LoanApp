using LoanApplication.Entities;
using System.Threading.Tasks;


namespace LoanApplication.Repos.UserRepo
{
    public interface IUserRepository : LoanApplication.Repos.Base.IRepoBase<User, int>
    {
        //Add User-specific methods here if needed
    }
}

using LoanApplication.Entities;

namespace LoanApplication.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
}

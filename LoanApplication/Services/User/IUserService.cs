namespace LoanApplication.Services;

public interface IUserService
{
    Task<bool> Add(User user);
    Task<User> Get(int id);
}

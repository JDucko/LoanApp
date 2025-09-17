namespace LoanApplication.Services.User
{
    public interface IUserService
    {
        Task<bool> Add(User user);
        Task<User> Get(int id);
    }
}
namespace LoanApplication.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Add(User user)
    {
        await _userRepository.Add(user);
        return true;
    }

    public async Task<User> Get(int id)
    {
        return await _userRepository.Get(id);
    }
}


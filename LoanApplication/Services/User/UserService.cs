using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Entities;
using LoanApplication.Repos.UserRepo;

namespace LoanApplication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.AddAsync(user);
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}


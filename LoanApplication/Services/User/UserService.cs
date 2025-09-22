using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Entities;
using LoanApplication.Repos.UserRepo;

namespace LoanApplication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            var created = await _userRepository.AddAsync(user);

            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user {FirstName} {LastName}", user.FirstName, user.LastName);
            throw;
        }
    }

    public async Task<User> GetUserAsync(int id)
    {

        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", id);
            throw new InvalidOperationException($"User with ID {id} not found.");
        }
        
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users;
    }
}


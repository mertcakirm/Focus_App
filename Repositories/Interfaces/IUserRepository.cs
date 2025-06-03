using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<User> RegisterAsync(User user);
}
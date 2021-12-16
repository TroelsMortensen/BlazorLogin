using BlazorLoginApp.Models;

namespace BlazorLoginApp.Services;

public interface IUserService
{
    public Task<User?> GetUserAsync(string username);
}
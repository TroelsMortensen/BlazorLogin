using System.Security.Claims;

namespace BlazorLoginApp.Authentication;

public interface IAuthService
{
    public Task LoginAsync(User user);
    public Task LogoutAsync();
    internal Task<ClaimsPrincipal> GetAuthAsync();

    internal Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
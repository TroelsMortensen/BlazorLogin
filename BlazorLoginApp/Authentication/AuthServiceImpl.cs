using System.Security.Claims;
using System.Text.Json;
using BlazorLoginApp.Models;
using BlazorLoginApp.Services;
using Microsoft.JSInterop;

namespace BlazorLoginApp.Authentication;

public class AuthServiceImpl : IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password)
    {
        User? user = await userService.GetUserAsync(username);

        ValidateLoginCredentials(password, user);

        await CacheUserAsync(user);

        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync();
        ClaimsPrincipal principal = CreateClaimsPrincipal(null);
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        User? user = null;
        if (!string.IsNullOrEmpty(userAsJson))
        {
            user = JsonSerializer.Deserialize<User>(userAsJson);
        }
        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        return principal;
    }

    private void ValidateLoginCredentials(string password, User? user)
    {
        if (user == null)
        {
            throw new Exception("Username not found");
        }

        if (!password.Equals(user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        ClaimsIdentity identity = new();
        if (user != null)
        {
            identity = ConvertUserToClaimsIdentity(user);
        }

        ClaimsPrincipal principal = new(identity);

        return principal;
    }

    private async Task CacheUserAsync(User? user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role),
            new Claim("SecurityLevel", user.SecurityLevel.ToString()),
            new Claim("BirthYear", user.BirthYear.ToString())
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
}
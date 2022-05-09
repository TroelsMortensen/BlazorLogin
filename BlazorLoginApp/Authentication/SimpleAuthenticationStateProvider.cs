﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorLoginApp.Authentication;

public class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthManager authManager;

    public SimpleAuthenticationStateProvider(IAuthManager authManager)
    {
        this.authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    // This method is called by Blazor framework whenever authentication or authorization needs to be evaluated.
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authManager.GetAuthAsync(); // get the user-as-ClaimsPrincipal from IAuthService
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}
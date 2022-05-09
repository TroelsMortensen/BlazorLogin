﻿using System.Security.Claims;

namespace BlazorLoginApp.Authentication;

public interface IAuthManager
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class AuthenticationService : AuthenticationStateProvider
{
    private AuthenticationState _authState;

    public AuthenticationService()
    {
        _authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(_authState);
    }

    public void Login(string username, string role)
    {
        var identity = new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    }, "apiauth_type");

        var user = new ClaimsPrincipal(identity);

        _authState = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }


    public void Logout()
    {
        _authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

using System.Data;
using System.Security.Claims;

namespace WebApi.Auth;

public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;

    public string? Name => _user?.Identity?.Name;

    private string _userId = string.Empty;

    public string GetUserId() =>
        IsAuthenticated()
            ? _user?.GetUserId() ?? string.Empty
            : _userId;

    public string? GetUserEmail() =>
        IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;

    public string? GetRoleType() => IsAuthenticated() ? _user!.GetRoleType() : string.Empty;

    public bool IsAuthenticated() =>
        _user?.Identity?.IsAuthenticated is true;

    public bool IsInRole(string role) =>
        _user?.IsInRole(role) is true;

    public IEnumerable<Claim>? GetUserClaims() =>
        _user?.Claims;

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        _user = user;
    }

    public void SetCurrentUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(_userId))
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        if (!string.IsNullOrEmpty(userId))
        {
            _userId = userId;
        }
    }

    public string GetNameKey()
    {
        return IsAuthenticated() ? $"{_user!.GetRoleType()}__{_user!.GetUserId()}__{_user!.GetEmail()}" : string.Empty;
    }
}

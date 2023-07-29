using System.Security.Claims;

namespace WebApi.Auth;

public interface ICurrentUser
{
    string? Name { get; }

    string GetUserId();

    string? GetUserEmail();

    string? GetRoleType();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();

    string GetNameKey();
}

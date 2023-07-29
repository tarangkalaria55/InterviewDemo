using WebApi.Shared.Authorization;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirstValueData(ClaimTypes.Email);

    public static string? GetFirstName(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Name)?.Value;

    public static string? GetSurname(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Surname)?.Value;

    public static string? GetPhoneNumber(this ClaimsPrincipal principal)
        => principal.FindFirstValueData(ClaimTypes.MobilePhone);

    public static string? GetUserId(this ClaimsPrincipal principal)
       => principal.FindFirstValueData(ClaimTypes.NameIdentifier);

    public static string? GetRoleType(this ClaimsPrincipal principal)
       => principal.FindFirstValueData(AuthClaims.RoleType);

    public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal)
    {
        return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
            principal.FindFirstValueData(AuthClaims.Expiration)));
    }

    private static string? FindFirstValueData(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
}

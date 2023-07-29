using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Exceptions;
using WebApi.Interfaces;
using WebApi.Models.Helper;
using WebApi.Persistence.Entities;
using WebApi.Settings;
using WebApi.Shared.Authorization;

namespace WebApi.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly AppSetting setting;
    public TokenRepository(AppSetting setting)
    {
        this.setting = setting;
    }

    public async Task<TokenResponse> GenerateTokens(int id, string username, bool isCustomer)
    {
        string token = GenerateJwt(id, username, isCustomer);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(setting.Jwt.ExpireInMinutes);

        var tokenResp = new TokenResponse(token, refreshToken, refreshTokenExpiryTime);
        return await Task.FromResult(tokenResp);
    }

    private string GenerateJwt(int id, string username, bool isCustomer)
    {
        var credentials = GetSigningCredentials();
        var claims = GetClaims(id, username, isCustomer);
        return GenerateEncryptedToken(credentials, claims);
    }


    private IEnumerable<Claim> GetClaims(int id, string username, bool isCustomer)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Convert.ToString(id)),
            new(ClaimTypes.Email, username ?? string.Empty),
            new(AuthClaims.Username, $"{username ?? string.Empty}"),
            new(AuthClaims.RoleType, isCustomer ? "CUSTOMER" : "SELLER"),
            new(ClaimTypes.Role, isCustomer ? "CUSTOMER" : "SELLER")
        };
        return claims;
    }


    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(setting.Jwt.ExpireInMinutes),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Jwt.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new UnauthorizedException("Invalid Token.");
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        byte[] secret = Encoding.UTF8.GetBytes(setting.Jwt.Key);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
}

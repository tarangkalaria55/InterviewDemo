namespace WebApi.Models.Helper;

public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);

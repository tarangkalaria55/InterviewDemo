using WebApi.Common.Interfaces;
using WebApi.Models.Helper;

namespace WebApi.Interfaces;

public interface ITokenRepository : ITransientService
{
    Task<TokenResponse> GenerateTokens(int id, string username, string roleType);
}

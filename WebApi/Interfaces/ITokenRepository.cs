using WebApi.Common.Interfaces;
using WebApi.Models.Helper;
using WebApi.Persistence.Entities;

namespace WebApi.Interfaces;

public interface ITokenRepository : ITransientService
{
    Task<TokenResponse> GenerateTokens(int id, string username, bool isCustomer);
}

using WebApi.Auth;
using WebApi.Caching;
using WebApi.Common.Interfaces;
using WebApi.SignalR;

namespace WebApi.Interfaces;

public interface IUnitOfWork : ITransientService
{
    IServiceProvider ServiceProvider { get; }
    ICurrentUser CurrentUser { get; }
    ITokenRepository TokenRepository { get; }
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
    IMessageRepository MessageRepository { get; }
}

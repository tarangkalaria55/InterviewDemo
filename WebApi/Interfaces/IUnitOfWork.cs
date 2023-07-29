using WebApi.Auth;
using WebApi.Common.Interfaces;

namespace WebApi.Interfaces;

public interface IUnitOfWork : ITransientService
{
    IServiceProvider ServiceProvider { get; }
    ICurrentUser CurrentUser { get; }
    ITokenRepository TokenRepository { get; }
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
    IMessageRepository MessageRepository { get; }

    ICurrentUserInitializer CurrentUserInitializer { get; }
}

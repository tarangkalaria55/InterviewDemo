using WebApi.Auth;
using WebApi.Caching;
using WebApi.Interfaces;
using WebApi.SignalR;

namespace WebApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider
    {
        get { return _serviceProvider; }
    }

    public ICurrentUser CurrentUser
    {
        get { return _serviceProvider.GetRequiredService<ICurrentUser>(); }
    }

    public ITokenRepository TokenRepository
    {
        get { return _serviceProvider.GetRequiredService<ITokenRepository>(); }
    }

    public IUserRepository UserRepository
    {
        get { return _serviceProvider.GetRequiredService<IUserRepository>(); }
    }

    public IBookRepository BookRepository
    {
        get { return _serviceProvider.GetRequiredService<IBookRepository>(); }
    }

    public IMessageRepository MessageRepository
    {
        get { return _serviceProvider.GetRequiredService<IMessageRepository>(); }
    }

}

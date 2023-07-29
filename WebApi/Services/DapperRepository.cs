using System.Data;
using WebApi.Interfaces;
using WebApi.Persistence.Context;
using Dapper;

namespace WebApi.Services;

public class DapperRepository : IDapperRepository
{
    private readonly BookContext _dbContext;

    public DapperRepository(BookContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class =>
        (await _dbContext.Connection.QueryAsync<T>(sql, param, transaction))
            .AsList();

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class
    {
        return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class
    {
        return _dbContext.Connection.QuerySingleAsync<T>(sql, param, transaction);
    }
}
using WebApi.Common.Interfaces;
using WebApi.Persistence.Entities;

namespace WebApi.Interfaces;

public interface IUserRepository : ITransientService
{
    Task<Customer> GetCustomerByUsername(string username, string password);

    Task<bool> AddCustomer(string username, string password);

    Task<Seller> GetSellerByUsername(string username, string password);

}

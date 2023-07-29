using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Exceptions;
using WebApi.Interfaces;
using WebApi.Persistence.Context;
using WebApi.Persistence.Entities;

namespace WebApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly BookContext _ctx;
    public UserRepository(BookContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Customer> GetCustomerByUsername(string username, string password)
    {
        var customers = _ctx.Customers.Where(m => m.Username == username);
        if (!(await customers.AnyAsync()))
        {
            var errors = new List<string>() { "user does not exists." };
            throw new CustomValidation("Authentication Failed.", errors);
        }

        var customer = customers.Where(m => m.Password == password);

        if (!(await customer.AnyAsync()))
        {
            var errors = new List<string>() { "username and password does not match." };
            throw new CustomValidation("Authentication Failed.", errors);
        }

        return await customer.SingleAsync();
    }

    public async Task<bool> AddCustomer(string username, string password)
    {
        var maxCustomerId = _ctx.Customers.Max(m => m.CustomerId);

        if (await _ctx.Customers.Where(m => m.Username == username).AnyAsync())
        {
            throw new ConflictException("Username exists");
        }

        var customer = new Customer()
        {
            CustomerId = maxCustomerId + 1,
            Username = username,
            Password = password
        };

        await _ctx.Customers.AddAsync(customer);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<Seller> GetSellerByUsername(string username, string password)
    {
        var sellers = _ctx.Sellers.Where(m => m.Username == username);
        if (!(await sellers.AnyAsync()))
        {
            var errors = new List<string>() { "user does not exists." };
            throw new CustomValidation("Authentication Failed.", errors);
        }

        var seller = sellers.Where(m => m.Password == password);

        if (!(await seller.AnyAsync()))
        {
            var errors = new List<string>() { "username and password does not match." };
            throw new CustomValidation("Authentication Failed.", errors);
        }

        return await seller.SingleAsync();
    }

}

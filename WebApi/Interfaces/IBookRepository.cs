using WebApi.Common.Interfaces;
using WebApi.Models.Other;
using WebApi.Persistence.Entities;

namespace WebApi.Interfaces
{
    public interface IBookRepository : ITransientService
    {
        Task<List<Book>> GetAvailableBook();
        Task<bool> RequestForPurchaseBook(int bookID);
        Task<List<GetCurrentOffers>> GetOffersBySellerID();
        Task<bool> AcceptOffersBySeller(int sellerID, int bookID, int customerID);
    }
}

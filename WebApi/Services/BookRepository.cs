using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Caching;
using WebApi.Exceptions;
using WebApi.Interfaces;
using WebApi.Models.Other;
using WebApi.Persistence.Context;
using WebApi.Persistence.Entities;
using WebApi.Shared.Authorization;
using WebApi.SignalR;

namespace WebApi.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _ctx;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoInMemory _userInfoInMemory;

        public BookRepository(BookContext ctx, IUnitOfWork unitOfWork, IUserInfoInMemory userInfoInMemory)
        {
            _ctx = ctx;
            _unitOfWork = unitOfWork;
            _userInfoInMemory = userInfoInMemory;
        }

        public async Task<List<Book>> GetAvailableBook()
        {
            return await _ctx.Books.Where(m => m.SoldToCustomer == null).ToListAsync();
        }

        public async Task<bool> RequestForPurchaseBook(int bookID)
        {
            var customerID = _unitOfWork.CurrentUser.GetUserId();
            var book = _ctx.Books.Where(m => m.SoldToCustomer == null && m.BookId == bookID);
            if (await book.AnyAsync())
            {
                var order = new Order()
                {
                    OrderId = 0,
                    BookId = bookID,
                    CustomerId = Convert.ToInt32(customerID),
                };

                await _ctx.Orders.AddAsync(order);

                await _ctx.SaveChangesAsync();

                var message = $"";

                var objBook = await book.Include(m => m.Seller).FirstOrDefaultAsync();

                if (objBook != null)
                {
                    await _unitOfWork.MessageRepository.SendDirectMessage(Roles.SELLER, Convert.ToString(objBook.SellerId), objBook.Seller.Username ?? "", message);
                }

                return true;

            }
            else
            {
                throw new CustomValidation("Book not available");
            }
        }

        public async Task<List<GetCurrentOffers>> GetOffersBySellerID()
        {

            var sellerID = _unitOfWork.CurrentUser.GetUserId();
            var id = Convert.ToInt32(sellerID);

            var query = from o in _ctx.Orders
                        join b in _ctx.Books on o.BookId equals b.BookId
                        join c in _ctx.Customers on o.CustomerId equals c.CustomerId
                        where (b.SellerId == id && b.SoldToCustomer == null)
                        select new GetCurrentOffers()
                        {
                            BookTitle = b.Title,
                            BookId = b.BookId,
                            CustomerID = c.CustomerId,
                            CustomerName = c.Username
                        };

            var currentOffers = await query.ToListAsync();

            return currentOffers;
        }

        public async Task<bool> AcceptOffersBySeller(int sellerID, int bookID, int customerID)
        {
            var isSellerExist = await _ctx.Sellers.AnyAsync(o => o.SellerId <= sellerID);
            if (!isSellerExist)
            {
                throw new CustomValidation("Seller does not exist");
            }
            var isBookExist = await _ctx.Books.AnyAsync(m => m.BookId == bookID && m.SellerId == sellerID && m.SoldToCustomer == null);
            if (!isBookExist)
            {
                throw new CustomValidation("Book does not exist");
            }
            var customer = _ctx.Customers.Where(m => m.CustomerId == customerID);

            if (!(await customer.AnyAsync()))
            {
                throw new CustomValidation("Customer does not exist");
            }

            var order = await _ctx.Orders.Where(m => m.BookId == bookID && m.CustomerId == customerID).FirstOrDefaultAsync();
            if (order != null)
            {
                var book = await _ctx.Books.Where(m => m.BookId == bookID).SingleAsync();
                book.SoldToCustomer = customerID;

                await _ctx.SaveChangesAsync();

                var message = $"";


                await _unitOfWork.MessageRepository.SendDirectMessage(Roles.CUSTOMER, Convert.ToString(customerID), customer.FirstOrDefault()?.Username ?? "", message);

                return true;

            }
            else
            {
                throw new NotFoundException("No order found");
            }
        }


    }
}

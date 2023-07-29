using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Interfaces;
using WebApi.Models.Request;
using WebApi.Shared.Authorization;

namespace WebApi.Controllers;

[Authorize(Roles = Roles.CUSTOMER)]
public class CustomerController : BaseApiController
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerController(ILogger<CustomerController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("GetAvailableBook")]
    public async Task<IActionResult> GetAvailableBook()
    {

        var books = await _unitOfWork.BookRepository.GetAvailableBook();
        return Ok(books);
    }

    [HttpPost("RequestForPurchaseBook")]
    public async Task<IActionResult> RequestForPurchaseBook(ReqRequestForPurchaseBook reqModel)
    {
        await new ReqRequestForPurchaseBookValidator().ValidateAndThrowAsync(reqModel);

        var flag = await _unitOfWork.BookRepository.RequestForPurchaseBook(reqModel.BookId ?? default!);
        return Ok(new { Status = "Success", Message = "Request for purchase placed successfully" });
    }

    
}

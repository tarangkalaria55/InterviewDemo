using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models.Request;
using WebApi.Shared.Authorization;

namespace WebApi.Controllers;

[Authorize(Roles = Roles.SELLER)]
public class SellerController : BaseApiController
{
    private readonly ILogger<SellerController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SellerController(ILogger<SellerController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("GetCurrentOffersBySellerID")]
    public async Task<IActionResult> GetCurrentOffersBySellerID()
    {
        //await new ReqGetCurrentOffersBySellerIDValidator().ValidateAndThrowAsync(reqModel);
        var offers = await _unitOfWork.BookRepository.GetOffersBySellerID();
        return Ok(offers);
    }

    [HttpPost("AcceptPurchaseOffer")]
    public async Task<IActionResult> AcceptPurchaseOffer(ReqAcceptPurchaseOffer reqModel)
    {
        await new ReqAcceptPurchaseOfferValidator().ValidateAndThrowAsync(reqModel);
        var flag = await _unitOfWork.BookRepository.AcceptOffersBySeller(reqModel.SellerId ?? default!, reqModel.BookId ?? default!, reqModel.CustomerId ?? default!);
        return Ok(new { Status = "Success", Message = "Offer accepted successfully" });
    }
}

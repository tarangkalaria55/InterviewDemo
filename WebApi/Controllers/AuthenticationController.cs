using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models.Request;
using WebApi.Shared.Authorization;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AuthenticationController : BaseApiController
{

    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationController(ILogger<AuthenticationController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("CustomerSignin")]
    public async Task<IActionResult> CustomerSignin(ReqSignin reqModel)
    {
        await new ReqSigninValidator().ValidateAndThrowAsync(reqModel);

        var customer = await _unitOfWork.UserRepository.GetCustomerByUsername(reqModel.Username ?? "", reqModel.Password ?? "");

        var token = await _unitOfWork.TokenRepository.GenerateTokens(customer.CustomerId, customer.Username ?? "", Roles.CUSTOMER);

        return Ok(new { CustomerID = customer!.CustomerId, Username = customer!.Username, Token = token.Token, TokenExpiryTime = token.RefreshTokenExpiryTime });
    }

    [HttpPost("CustomerSignup")]
    public async Task<IActionResult> CustomerSignup(ReqSignup reqModel)
    {
        await new ReqSignupValidator().ValidateAndThrowAsync(reqModel);

        await _unitOfWork.UserRepository.AddCustomer(reqModel.Username!, reqModel.Password!);

        return Ok(new { Status = "Success", Message = "User created successfully" });
    }

    [HttpPost("SellerSignin")]
    public async Task<IActionResult> SellerSignin(ReqSignin reqModel)
    {
        await new ReqSigninValidator().ValidateAndThrowAsync(reqModel);

        var seller = await _unitOfWork.UserRepository.GetSellerByUsername(reqModel.Username ?? "", reqModel.Password ?? "");

        var token = await _unitOfWork.TokenRepository.GenerateTokens(seller.SellerId, seller.Username ?? "", Roles.SELLER);

        return Ok(new { SellerId = seller!.SellerId, Username = seller!.Username, Token = token.Token, TokenExpiryTime = token.RefreshTokenExpiryTime });
    }

    //[HttpPost("Signup")]
    //public async Task<IActionResult> Signup(ReqSignup reqModel)
    //{
    //    await new ReqSignupValidator().ValidateAndThrowAsync(reqModel);

    //    await _unitOfWork.UserRepository.AddCustomer(reqModel.Username!, reqModel.Password!);

    //    return Ok(new { Status = "Success", Message = "User created successfully" });
    //}
}
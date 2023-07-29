using FluentValidation;
using WebApi.Common.Interfaces;

namespace WebApi.Models.Request;

public class ReqAcceptPurchaseOffer
{
    public int? SellerId { get; set; }
    public int? BookId { get; set; }
    public int? CustomerId { get; set; }
}


public class ReqAcceptPurchaseOfferValidator : CustomValidator<ReqAcceptPurchaseOffer>
{
    public ReqAcceptPurchaseOfferValidator()
    {
        RuleFor(x => x.SellerId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.BookId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.CustomerId).NotNull().NotEmpty().GreaterThan(0);
    }
}

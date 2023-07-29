using WebApi.Common.Interfaces;

namespace WebApi.Models.Request;

public class ReqGetCurrentOffersBySellerID
{
    public int? SellerId { get; set; }
}

public class ReqGetCurrentOffersBySellerIDValidator : CustomValidator<ReqGetCurrentOffersBySellerID>
{
    public ReqGetCurrentOffersBySellerIDValidator()
    {
        RuleFor(x => x.SellerId).NotNull().NotEmpty().GreaterThan(0);
    }
}

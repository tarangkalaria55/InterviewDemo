using WebApi.Common.Interfaces;

namespace WebApi.Models.Request;

public class ReqRequestForPurchaseBook
{
    public int? BookId { get; set; }
}

public class ReqRequestForPurchaseBookValidator : CustomValidator<ReqRequestForPurchaseBook>
{
    public ReqRequestForPurchaseBookValidator()
    {
        RuleFor(x => x.BookId).NotNull().NotEmpty().GreaterThan(0);
    }
}

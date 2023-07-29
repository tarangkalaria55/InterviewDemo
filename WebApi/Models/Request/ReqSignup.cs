using WebApi.Common.Interfaces;

namespace WebApi.Models.Request;

public class ReqSignup
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class ReqSignupValidator : CustomValidator<ReqSignup>
{
    public ReqSignupValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}


using WebApi.Common.Interfaces;

namespace WebApi.Models.Request;

public class ReqSignin
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class ReqSigninValidator : CustomValidator<ReqSignin>
{
    public ReqSigninValidator() { 
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

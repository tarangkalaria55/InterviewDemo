using System.Net;

namespace WebApi.Exceptions
{
    public class CustomValidation : CustomException
    {
        public CustomValidation(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, errors, statusCode)
        {
        }
    }
}

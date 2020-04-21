using MediatR;

namespace Services.Utility
{
    public class ValidateRecaptchaCommand : IRequest<bool>
    {
        public string Response { get; set; }
    }
}

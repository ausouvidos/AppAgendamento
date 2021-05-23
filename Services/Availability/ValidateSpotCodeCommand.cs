using MediatR;
using Models;

namespace Services.Availability
{
    public class ValidateSpotCodeCommand : IRequest<ApiResponse>
    {
        public string Code { get; set; }
    }
}

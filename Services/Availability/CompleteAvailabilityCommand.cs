using MediatR;
using Models;

namespace Services.Availability
{
    public class RemoveAvailabilityCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}

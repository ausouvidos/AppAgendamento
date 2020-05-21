using MediatR;
using Models;

namespace Services.Availability
{
    public class FulfillAvailabilityCommand : IRequest<string>
    {
        public string EventId { get; set; }
    }
}

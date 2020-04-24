using System;
using MediatR;

namespace Services.Availability
{
    public class CanAddAvailabilityCommand : IRequest<bool>
    {
        public Guid UserIdentityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

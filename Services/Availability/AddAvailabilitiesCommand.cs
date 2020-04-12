using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class AddAvailabilitiesCommand : IRequest<bool>
    {
        public Guid UserIdentityId { get; set; }
        public IList<AvailabilityDates> Dates { get; set; }

    }
}

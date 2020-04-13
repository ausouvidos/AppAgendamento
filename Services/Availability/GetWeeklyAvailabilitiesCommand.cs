using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitiesCommand : IRequest<IEnumerable<Models.AvailabilityDates>>
    {
        public DateTime RefDate { get; set; }
    }
}

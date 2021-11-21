using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitiesCommand : IRequest<IEnumerable<Models.AvailabilityDates>>
    {
        public DateTime RefDate { get; set; }
        public String? Code { get; set; }
        public String? Email { get; set; }
    }
}

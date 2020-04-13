using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetSpecificAvailabilityCommand : IRequest<Models.Availability>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

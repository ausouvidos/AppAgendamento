using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetSpecificAvailabilityCommand : IRequest<Models.Availability>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Guid> Guids { get; set; }
    }
}

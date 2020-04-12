using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetAvailabilitiesCommand : IRequest<IEnumerable<Models.Availability>>
    {
        public Guid UserIdentityId { get; set; }
    }
}

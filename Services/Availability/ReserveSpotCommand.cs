using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Availability
{
    public class ReserveSpotCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

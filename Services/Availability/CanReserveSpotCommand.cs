using System;
using MediatR;

namespace Services.Availability
{
    public class CanReserveSpotCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}

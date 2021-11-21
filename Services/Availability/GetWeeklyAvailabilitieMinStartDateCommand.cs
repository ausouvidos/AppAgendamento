using System;
using MediatR;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitieMinStartDateCommand : IRequest<DateTime>
    {
        public string Email { get; set; }
    }
}

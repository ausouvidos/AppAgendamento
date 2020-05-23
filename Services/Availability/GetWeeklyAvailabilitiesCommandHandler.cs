using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using MediatR;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitiesCommandHandler : RequestHandler<GetWeeklyAvailabilitiesCommand, IEnumerable<Models.AvailabilityDates>>
    {
        private readonly AusOuvidosContext _db;

        public GetWeeklyAvailabilitiesCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        protected override IEnumerable<Models.AvailabilityDates> Handle(GetWeeklyAvailabilitiesCommand request) =>
             _db.Availabilities
             .Where(a => a.Start > DateTime.UtcNow && a.Start >= request.RefDate.FirstDayOfWeek().ToUniversalTime() && a.Start <= request.RefDate.LastDayOfWeek().AddDays(1).ToUniversalTime() && a.IsFree)
             .OrderBy(a => a.Start)
             .ToList()
             .Select(a => new Models.AvailabilityDates
             {
                 Start = a.Start,
                 End = a.End
             })
             .Distinct();
    }
}

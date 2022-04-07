using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Services.Utility;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitieMinStartDateCommandHandler : IRequestHandler<GetWeeklyAvailabilitieMinStartDateCommand, DateTime>
    {
        private readonly AusOuvidosContext _db;

        public GetWeeklyAvailabilitieMinStartDateCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }

        public Task<DateTime> Handle(GetWeeklyAvailabilitieMinStartDateCommand request, CancellationToken cancellationToken)
        {
            var lastAppointment = _db.Availabilities.Where(a => a.CustomerEmail == request.Email).OrderByDescending(a => a.Start).FirstOrDefault();
            DateTime minStart = DateTime.UtcNow.AddHours(1);
            return Task.FromResult(minStart);
            //if (lastAppointment == null || lastAppointment.Start.AddHours(72) < minStart)
            //{
            //    return Task.FromResult(minStart);
            //}
            //return Task.FromResult(lastAppointment.Start.AddHours(72));
        }
    }
}

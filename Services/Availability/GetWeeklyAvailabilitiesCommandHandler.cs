using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetWeeklyAvailabilitiesCommandHandler : IRequestHandler<GetWeeklyAvailabilitiesCommand, IEnumerable<AvailabilityDates>>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public GetWeeklyAvailabilitiesCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public async Task<IEnumerable<AvailabilityDates>> Handle(GetWeeklyAvailabilitiesCommand request, CancellationToken cancellationToken) {

            DateTime minStart = DateTime.UtcNow.AddHours(1);
            if(!string.IsNullOrEmpty(request.Email))
            {
                minStart = await _mediator.Send(new GetWeeklyAvailabilitieMinStartDateCommand { Email = request.Email });
            }


            IQueryable<Models.Availability> availabilities = _db.Availabilities.Where(a => a.Start > minStart && a.Start >= request.RefDate.FirstDayOfWeek().ToUniversalTime() && a.Start <= request.RefDate.LastDayOfWeek().AddDays(1).ToUniversalTime() && a.IsFree);

            if (!string.IsNullOrEmpty(request.Code))
            {
                var voucher = await _mediator.Send(new GetVoucherCommand { Voucher = request.Code.Trim() });
                if(voucher != null)
                {
                    IEnumerable<Guid> guids = await _mediator.Send(new GetVoucherProfessionalsCommand { VoucherId = voucher.Id });
                    if(guids.Count() > 0)
                    {
                        availabilities = availabilities.Where(a => guids.Contains(a.UserId));
                    }
                }
            }

            return availabilities
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
}

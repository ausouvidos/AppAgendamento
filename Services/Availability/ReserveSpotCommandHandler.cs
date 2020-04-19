using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;

namespace Services.Availability
{
    public class ReserveSpotCommandHandler : IRequestHandler<ReserveSpotCommand, bool>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public ReserveSpotCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public async Task<bool> Handle(ReserveSpotCommand request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var availability = await _mediator.Send(new GetSpecificAvailabilityCommand { StartDate = request.Start, EndDate = request.End });
            if (availability != null)
            {
                try
                {
                    availability.CustomerEmail = request.Email;
                    availability.CustomerName = request.Name;
                    availability.CustomerMobile = request.Mobile;
                    availability.IsFree = false;

                    _db.Availabilities.Update(availability);
                    await _db.SaveChangesAsync();

                    await _mediator.Publish(new ReservedSpotNotification { Availability = availability });

                    scope.Complete();

                    return true;
                }
                catch { }
            }

            return false;
        }

    }
}

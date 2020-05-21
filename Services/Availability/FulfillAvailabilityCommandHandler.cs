using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Models;
using Services.Utility;

namespace Services.Availability
{
    public class FulfillAvailabilityCommandHandler : IRequestHandler<FulfillAvailabilityCommand, string>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public FulfillAvailabilityCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public async Task<string> Handle(FulfillAvailabilityCommand request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var availability = _db.Availabilities.FirstOrDefault(a => a.EventId == request.EventId);
            if (availability != null)
            {
                if (!availability.IsFulfilled)
                {
                    availability.IsFulfilled = true;
                    availability.FulfillmentDate = DateTime.UtcNow;

                    _db.Availabilities.Update(availability);
                    await _db.SaveChangesAsync();
                    scope.Complete();
                }

                return availability.EventUrl;
            }
            return null;
        }
    }
}

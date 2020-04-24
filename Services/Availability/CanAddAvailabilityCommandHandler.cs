using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Services.Utility;

namespace Services.Availability
{
    public class CanAddAvailabilityCommandHandler : IRequestHandler<CanAddAvailabilityCommand, bool>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public CanAddAvailabilityCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public Task<bool> Handle(CanAddAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(!_db.Availabilities.Any(a => a.UserId == request.UserIdentityId && (a.Start == request.Start || a.End == request.End)));
        }
    }
}

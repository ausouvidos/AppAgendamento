using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Services.Utility;

namespace Services.Availability
{
    public class CanReserveSpotCommandHandler : IRequestHandler<CanReserveSpotCommand, bool>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public CanReserveSpotCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public Task<bool> Handle(CanReserveSpotCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(!_db.Availabilities.Any(a => a.CustomerEmail == request.Email && (a.Start == request.Date || a.End == request.Date)));
        }
    }
}

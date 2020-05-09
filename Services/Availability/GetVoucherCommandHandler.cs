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
    public class GetVoucherCommandHandler : IRequestHandler<GetVoucherCommand, Voucher>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public GetVoucherCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public Task<Voucher> Handle(GetVoucherCommand request, CancellationToken cancellationToken) =>
            Task.FromResult(_db.Vouchers.FirstOrDefault(a => a.Token == request.Voucher && a.RemainingRedeemCount > 0 && a.Company.Status == Status.APPROVED));
    }
}

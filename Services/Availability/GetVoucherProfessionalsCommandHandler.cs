using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Collections.Generic;
using System;
using Data;
using MediatR;
using Models;
using Services.Utility;


namespace Services.Availability
{
    public class GetVoucherProfessionalsCommandHandler : IRequestHandler<GetVoucherProfessionalsCommand, IEnumerable<Guid>>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public GetVoucherProfessionalsCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public Task<IEnumerable<Guid>> Handle(GetVoucherProfessionalsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Guid> output = _db.VoucherProfessionals
                .Where(v => v.VuocherId == request.VoucherId)
                .Select(v => v.UserId)
                .ToList();

            return Task.FromResult(output.AsEnumerable());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;
using Models.Identity;
using Services.Utility;

namespace Services.Company
{
    public class GetPendingApprovalCompaniesCommandHandler : IRequestHandler<GetPendingApprovalCompaniesCommand, IEnumerable<Models.Company>>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public GetPendingApprovalCompaniesCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public Task<IEnumerable<Models.Company>> Handle(GetPendingApprovalCompaniesCommand request, CancellationToken cancellationToken) =>
            Task.FromResult<IEnumerable<Models.Company>>(_db.Companies.Where(a => a.Status == Status.PENDING).ToList());
    }
}

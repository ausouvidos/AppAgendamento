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
    public class ApproveCompanyCommandHandler : IRequestHandler<ApproveCompanyCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public ApproveCompanyCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public async Task<ApiResponse> Handle(ApproveCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _db.Companies.FindAsync(request.Id);
            if (company != null && company.Status == Status.PENDING)
            {
                company.Status = Status.APPROVED;

                var voucher = new Voucher();
                voucher.CompanyId = company.Id;
                voucher.RemainingRedeemCount = request.Quantidade;
                voucher.Token = Guid.NewGuid().ToString();

                _db.Companies.Update(company);
                await _db.Vouchers.AddAsync(voucher);
                await _db.SaveChangesAsync();
                return ApiResponse.Success();

            }
            return ApiResponse.Error("Empresa não encontrada");
        }
    }
}

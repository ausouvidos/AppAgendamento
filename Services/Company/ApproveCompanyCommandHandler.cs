using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        private static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        private string GenerateVoucherCode()
        {
            var generator = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            generator.GetBytes(bytes);
            var code = ByteArrayToHexString(bytes);

            if (_db.Vouchers.Any(a => a.Token == code))
            {
                return GenerateVoucherCode();
            }
            else
            {
                return code;
            }
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

                voucher.Token = GenerateVoucherCode();

                _db.Companies.Update(company);
                await _db.Vouchers.AddAsync(voucher);
                await _db.SaveChangesAsync();
                return ApiResponse.Success();

            }
            return ApiResponse.Error("Empresa não encontrada");
        }
    }
}

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
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public AddCompanyCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public async Task<ApiResponse> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var passedRecaptcha = await _mediator.Send(new ValidateRecaptchaCommand { Response = request.RecaptchaResponse });
            if (!passedRecaptcha)
            {
                return ApiResponse.Error("Captcha inválido");
            }

            var company = new Models.Company
            {
                Address = request.Address,
                City = request.City,
                ContactPerson = request.ContactPerson,
                District = request.District,
                State = request.State,
                Name = request.Name,
                Phone = request.Phone,
                ZipCode = request.ZipCode,

                Status = Status.PENDING,
                Created = DateTime.UtcNow,
            };


            await _db.Companies.AddAsync(company);
            await _db.SaveChangesAsync();
            return ApiResponse.Success();
        }
    }
}

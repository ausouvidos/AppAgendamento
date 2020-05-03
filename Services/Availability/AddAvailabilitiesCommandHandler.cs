using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;
using Models.Identity;

namespace Services.Availability
{
    public class AddAvailabilitiesCommandHandler : IRequestHandler<AddAvailabilitiesCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public AddAvailabilitiesCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public async Task<ApiResponse> Handle(AddAvailabilitiesCommand request, CancellationToken cancellationToken)
        {
            var items = new List<Models.Availability>();
            foreach (var av in request?.Dates?.ToList())
            {
                if (await _mediator.Send(new CanAddAvailabilityCommand { UserIdentityId = request.UserIdentityId, Start = av.Start, End = av.End }))
                {
                    items.Add(new Models.Availability
                    {
                        IsFree = true,
                        Start = av.Start,
                        End = av.End,
                        UserId = request.UserIdentityId
                    });
                }
            }

            if (items.Any())
            {
                await _db.Availabilities.AddRangeAsync(items);
                await _db.SaveChangesAsync();
                return ApiResponse.Success();
            }

            return ApiResponse.Error("Já existe um registro para esse mesmo horário. Por favor, selecione outro e tente novamente.");
        }
    }
}

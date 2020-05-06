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
    public class RemoveAvailabilityCommandHandler : IRequestHandler<RemoveAvailabilityCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public RemoveAvailabilityCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public async Task<ApiResponse> Handle(RemoveAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var availability = await _db.Availabilities.FindAsync(new object[] { request.Id });
            if (availability != null && availability.IsFree)
            {
                _db.Availabilities.Remove(availability);
                await _db.SaveChangesAsync();
                return ApiResponse.Success();
            }

            return ApiResponse.Error("O Horário selecionado não pode ser removido.");
        }
    }
}

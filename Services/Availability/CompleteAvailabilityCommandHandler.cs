using System;
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
    public class CompleteAvailabilityCommandHandler : IRequestHandler<CompleteAvailabilityCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public CompleteAvailabilityCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }
        public async Task<ApiResponse> Handle(CompleteAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var availability = await _db.Availabilities.FindAsync(new object[] { request.Id });
            if (availability != null && !availability.IsFree)
            {
                availability.IsCompleted = true;
                availability.Observations = request.Observacoes;
                availability.Notes = request.Notas;
                if (availability.CompletedDate == DateTime.MinValue)
                {
                    availability.CompletedDate = DateTime.UtcNow;
                }

                _db.Availabilities.Update(availability);
                await _db.SaveChangesAsync();
                return ApiResponse.Success();
            }

            return ApiResponse.Error("O agendamento selecionado não pode ser concluído.");
        }
    }
}

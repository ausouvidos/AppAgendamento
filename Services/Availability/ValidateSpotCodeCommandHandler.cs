using System;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;

namespace Services.Availability
{
    public class ValidateSpotCodeCommandHandler : IRequestHandler<ValidateSpotCodeCommand, ApiResponse>
    {
        private readonly IMediator _mediator;

        public ValidateSpotCodeCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<ApiResponse> Handle(ValidateSpotCodeCommand request, CancellationToken cancellationToken)
        {
            var canReserveToday = await _mediator.Send(new CanReserveTodayCommand { Email = request.Email.Trim() });
            if(!canReserveToday)
            {
                return ApiResponse.Error("Um novo agendamente só pode ser realizado 72 horas após seu último agendamento");
            }

            var voucher = await _mediator.Send(new GetVoucherCommand { Voucher = request.Code.Trim() });
            if(voucher != null && voucher.RemainingRedeemCount > 0)
            {
                return ApiResponse.Success();
            }
            return ApiResponse.Error("Não foi possível executar a validar.");
        }
    }
}

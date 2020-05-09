using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Models;
using Services.Utility;

namespace Services.Availability
{
    public class ReserveSpotCommandHandler : IRequestHandler<ReserveSpotCommand, ApiResponse>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;

        public ReserveSpotCommandHandler(AusOuvidosContext db, IMediator mediator)
        {
            this._db = db;
            this._mediator = mediator;
        }

        public async Task<ApiResponse> Handle(ReserveSpotCommand request, CancellationToken cancellationToken)
        {
            var passedRecaptcha = await _mediator.Send(new ValidateRecaptchaCommand { Response = request.RecaptchaResponse });
            if (!passedRecaptcha)
            {
                return ApiResponse.Error("Captcha inválido");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            var voucher = await _mediator.Send(new GetVoucherCommand { Voucher = request.Voucher });
            if (voucher != null)
            {
                var canReserveSpot = await _mediator.Send(new CanReserveSpotCommand { Email = request.Email, Date = request.Start });
                if (canReserveSpot)
                {
                    var availability = await _mediator.Send(new GetSpecificAvailabilityCommand { StartDate = request.Start, EndDate = request.End });
                    if (availability != null)
                    {
                        try
                        {
                            availability.CustomerEmail = request.Email;
                            availability.CustomerName = request.Name;
                            availability.CustomerMobile = request.Mobile;
                            availability.IsFree = false;
                            availability.VoucherId = voucher.Id;

                            _db.Availabilities.Update(availability);

                            await _mediator.Publish(new RedeemVoucherNotification { Voucher = voucher });

                            await _db.SaveChangesAsync();

                            await _mediator.Publish(new ReservedSpotNotification { Availability = availability });

                            scope.Complete();

                            return ApiResponse.Success();
                        }
                        catch { }
                    }
                    else
                    {
                        return ApiResponse.Error("O horário selecionado já foi reservado por outra pessoa. Por favor, escolha um novo horário.");
                    }
                }
                else
                {
                    return ApiResponse.Error("Já existe uma reserva para esse usuário");
                }
            }
            else
            {
                return ApiResponse.Error("O Voucher é inválido ou está expirado.");
            }

            return ApiResponse.Error("Um erro aconteceu ao fazer o agendamento. Por favor, tente novamente mais tarde");
        }
    }
}

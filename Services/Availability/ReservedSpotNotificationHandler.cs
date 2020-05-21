using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Microsoft.Extensions.Options;
using Models;

namespace Services.Availability
{
    public class ReservedSpotNotificationHandler : INotificationHandler<ReservedSpotNotification>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;
        private readonly FlowConfig _flowConfig;

        public ReservedSpotNotificationHandler(AusOuvidosContext db, IMediator mediator, IHttpClientFactory clientFactory, IOptionsMonitor<FlowConfig> optionsAccessor)
        {
            this._db = db;
            this._mediator = mediator;
            this._clientFactory = clientFactory;
            this._flowConfig = optionsAccessor?.CurrentValue;
        }

        public async Task Handle(ReservedSpotNotification request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient("FlowClient");
            var data = new
            {
                action = "Agendar",
                key = "d6b561c23978453daaf088fad41b3d91",
                Paciente = request.Availability.CustomerName,
                Profissional = request.Availability.User.Email,
                AgendaInicio = request.Availability.Start,
                AgendaTermino = request.Availability.End,
                EmailPaciente = request.Availability.CustomerEmail,
                CelularPaciente = request.Availability.CustomerMobile,
                ID = request.Availability.Id,
                IDEvento = request.Availability.EventId
            };
            var json = JsonSerializer.Serialize(data);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync($"invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig={_flowConfig.FlowToken}", stringContent);
        }
    }
}

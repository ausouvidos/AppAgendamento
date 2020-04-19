using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;

namespace Services.Availability
{
    public class ReservedSpotNotificationHandler : INotificationHandler<ReservedSpotNotification>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;

        public ReservedSpotNotificationHandler(AusOuvidosContext db, IMediator mediator, IHttpClientFactory clientFactory)
        {
            this._db = db;
            this._mediator = mediator;
            this._clientFactory = clientFactory;
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
                Agenda = request.Availability.Start,
                EmailPaciente = request.Availability.CustomerEmail,
                Celular = request.Availability.CustomerMobile
            };
            var json = JsonSerializer.Serialize(data);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync("invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=tvC4FoJ7AGq_uKp6lGfVy0MM4RGneofp3d7vN0FoDSg", stringContent);
        }
    }
}

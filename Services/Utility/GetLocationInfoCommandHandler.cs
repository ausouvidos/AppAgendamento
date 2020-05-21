using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;

namespace Services.Utility
{
    public class GetLocationInfoCommandHandler : IRequestHandler<GetLocationInfoCommand, Location>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;

        public GetLocationInfoCommandHandler(AusOuvidosContext db, IMediator mediator, IHttpClientFactory clientFactory)
        {
            this._db = db;
            this._mediator = mediator;
            this._clientFactory = clientFactory;
        }

        public async Task<Location> Handle(GetLocationInfoCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.IP))
            {
                // TODO: Move to a config file
                var secret = "6622b7913c762ca28f896585207d189b";
                var httpClient = _clientFactory.CreateClient("IPStack");
                var res = await httpClient.GetAsync($"{request.IP}?access_key={secret}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<Location>(json);
                }
            }

            return null;
        }
    }
}

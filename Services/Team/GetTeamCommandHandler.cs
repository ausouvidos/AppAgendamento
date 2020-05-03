using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;

namespace Services.Team
{
    public class GetTeamCommandHandler : IRequestHandler<GetTeamCommand, IEnumerable<Professional>>
    {
        private readonly AusOuvidosContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public GetTeamCommandHandler(AusOuvidosContext db, IHttpClientFactory clientFactory)
        {
            this._db = db;
            this._clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Professional>> Handle(GetTeamCommand request, CancellationToken token)
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var result = await client.GetAsync("profissionais");
            result.EnsureSuccessStatusCode();
            using var responseStream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Professional>>(responseStream);
        }
    }
}

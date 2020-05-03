using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models;

namespace Services.Team
{
    public class GetTeamMemberPhotoCommandHandler : IRequestHandler<GetTeamMemberPhotoCommand, Image>
    {
        private readonly AusOuvidosContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public GetTeamMemberPhotoCommandHandler(AusOuvidosContext db, IHttpClientFactory clientFactory)
        {
            this._db = db;
            this._clientFactory = clientFactory;
        }

        public async Task<Image> Handle(GetTeamMemberPhotoCommand request, CancellationToken token)
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var result = await client.GetAsync($"profissionais/{request.MemberId}/photo");
            result.EnsureSuccessStatusCode();
            var contents = await result.Content.ReadAsByteArrayAsync();
            var filename = request.MemberId + Path.GetExtension(result.Content.Headers.ContentDisposition.FileName);

            return new Image
            {
                Contents = contents,
                Name = filename
            };
        }
    }
}

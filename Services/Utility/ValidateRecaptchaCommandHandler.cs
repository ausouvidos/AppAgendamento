using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;

namespace Services.Utility
{
    public class ValidateRecaptchaCommandHandler : IRequestHandler<ValidateRecaptchaCommand, bool>
    {
        private readonly AusOuvidosContext _db;
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;

        public ValidateRecaptchaCommandHandler(AusOuvidosContext db, IMediator mediator, IHttpClientFactory clientFactory)
        {
            this._db = db;
            this._mediator = mediator;
            this._clientFactory = clientFactory;
        }

        public async Task<bool> Handle(ValidateRecaptchaCommand request, CancellationToken cancellationToken)
        {
            var secret = "6LcAFuwUAAAAAKdAZOSYXBC49SRB2ShLtq5kwr4E";
            var httpClient = _clientFactory.CreateClient("RecaptchaClient");
            var res = await httpClient.GetAsync($"siteverify?secret={secret}&response={request.Response}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var jsonData = JsonSerializer.Deserialize<RecaptchaAPIResponse>(json);
                return jsonData.success;
            }

            return false;
        }
    }

    internal class RecaptchaAPIResponse
    {
        public bool success { get; set; }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Availability;

namespace AusOuvidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitiesController : BaseController
    {
        public AvailabilitiesController()
        {
        }

        [HttpPost]
        public async Task<bool> AddAvailabilities([FromBody]AddAvailabilitiesCommand request) =>
            await Mediator.Send(request);

        [HttpGet]

        public async Task<IEnumerable<Availability>> GetAvailabilities() => 
            await Mediator.Send(new GetAvailabilitiesCommand { UserIdentityId = GetCurrentUserId() });
    }
}

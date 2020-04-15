using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Availability;

namespace AusOuvidos.Controllers
{
    [ApiController]
    public class AvailabilitiesController : BaseController
    {
        public AvailabilitiesController()
        {
        }

        [HttpPost]
        public async Task<bool> Add([FromBody]AddAvailabilitiesCommand request)
        {
            request.UserIdentityId = GetCurrentUserId();
            return await Mediator.Send(request);
        }

        [HttpGet]

        public async Task<IEnumerable<Availability>> MyAvailabilities() =>
            await Mediator.Send(new GetAvailabilitiesCommand { UserIdentityId = GetCurrentUserId() });

        [AllowAnonymous]
        [HttpGet]
        [Route("{date}")]
        public async Task<IEnumerable<AvailabilityDates>> WeeklyAvailableSpots(DateTime date) =>
            await Mediator.Send(new GetWeeklyAvailabilitiesCommand { RefDate = date });

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> ReserveSpot([FromBody]ReserveSpotCommand request) =>
            await Mediator.Send(request);

    }
}

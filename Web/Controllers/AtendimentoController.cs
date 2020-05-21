using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Services.Availability;
using Services.Team;

namespace AusOuvidos.Controllers
{
    [Route("[controller]")]
    public class AtendimentoController : BaseController
    {
        public AtendimentoController(IMemoryCache memoryCache)
        {
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Fulfill(string id)
        {
            var url = await Mediator.Send(new FulfillAvailabilityCommand { EventId = id });
            if (!string.IsNullOrEmpty(url))
            {
                return RedirectPreserveMethod(url);
            }
            else
            {
                return LocalRedirect("/nao-encontradao");
            }
        }
    }
}

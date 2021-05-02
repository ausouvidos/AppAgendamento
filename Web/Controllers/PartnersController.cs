using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Services.Partner;

namespace AusOuvidos.Controllers
{
    public class PartnersController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public PartnersController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IEnumerable<Partner>> All() => await Mediator.Send(new GetPartnersCommand());

        [HttpGet]
        [Route("{partnerId}")]
        public async Task<IActionResult> Logo(int partnerId)
        {
            return await _memoryCache.GetOrCreateAsync(partnerId, async (entry) =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);

                var image = await Mediator.Send(new GetPartnerLogoCommand { Id = partnerId });
                return File(image.Contents, "application/octet-stream", image.Name);
            });
        }
    }
}

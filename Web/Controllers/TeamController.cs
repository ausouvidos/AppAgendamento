using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Services.Team;

namespace AusOuvidos.Controllers
{
    [ApiController]
    public class TeamController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public TeamController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }


        [HttpGet]
        public async Task<IEnumerable<Professional>> Members() =>
            await Mediator.Send(new GetTeamCommand());

        [HttpGet]
        [Route("{memberId}/photo")]
        public async Task<IActionResult> Members(int memberId)
        {
            return await _memoryCache.GetOrCreateAsync(memberId, async (entry) =>
            {
                var image = await Mediator.Send(new GetTeamMemberPhotoCommand { MemberId = memberId });
                return File(image.Contents, "application/octet-stream", image.Name);
            });
        }
    }
}

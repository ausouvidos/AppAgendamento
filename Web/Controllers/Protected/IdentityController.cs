using System.Threading.Tasks;
using AusOuvidos.Controllers.Protected;
using Microsoft.AspNetCore.Mvc;
using Services.Identity;
using System.Collections.Generic;
using Models.Identity;

namespace AusOuvidos.Controllers
{
    [ApiController]
    public class IdentityController : BaseProtectedController
    {
        public IdentityController()
        {
        }

        [HttpGet]
        public async Task<bool> IsAdmin() =>
            await Mediator.Send(new IsAdminUserCommand { UserIdentityId = GetCurrentUserId() });

        [HttpGet]
        public async Task<IEnumerable<User>> Users() => await Mediator.Send(new GetUserCommand { });
    }
}

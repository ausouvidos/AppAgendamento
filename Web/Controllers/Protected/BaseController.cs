using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AusOuvidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected Guid GetCurrentUserId() => new Guid(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
    }
}

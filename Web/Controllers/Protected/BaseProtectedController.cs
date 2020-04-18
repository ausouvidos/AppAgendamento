using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AusOuvidos.Controllers.Protected
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BaseProtectedController : BaseController
    {
        protected Guid GetCurrentUserId() => new Guid(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
    }
}

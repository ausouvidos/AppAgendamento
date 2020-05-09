using System.Threading.Tasks;
using AusOuvidos.Controllers.Protected;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Company;

namespace AusOuvidos.Controllers
{
    [ApiController]
    public class CompaniesController : BaseProtectedController
    {
        public CompaniesController()
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Add([FromBody] AddCompanyCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}

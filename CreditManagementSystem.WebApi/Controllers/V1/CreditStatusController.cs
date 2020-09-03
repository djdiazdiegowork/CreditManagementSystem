using CreditManagementSystem.Client.Model.CreditStatus;
using CreditManagementSystem.Common.Response;
using CreditManagementSystem.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    public class CreditStatusController : ControllerBase
    {
        private readonly ICreditStatusService _creditStatusService;

        public CreditStatusController(ICreditStatusService creditStatusService)
        {
            this._creditStatusService = creditStatusService;
        }

        /// <summary>
        /// Get all all CreditStatus.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this._creditStatusService.GetAll<CreditStatusDto>();

            var response = new Response<IEnumerable<CreditStatusDto>> {
                Body = result,
                Code = Response.StatusCode
            };

            return Ok(response);
        }
    }
}

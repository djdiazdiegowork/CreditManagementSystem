using CreditManagementSystem.Client.Model.RiskCenter;
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
    public class RiskCenterController : ControllerBase
    {
        private readonly IRiskCenterService _riskCenterService;

        public RiskCenterController(IRiskCenterService riskCenterService)
        {
            this._riskCenterService = riskCenterService;
        }

        /// <summary>
        /// Get all all Risk.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this._riskCenterService.GetAll<RiskCenterDto>();

            var response = new Response<IEnumerable<RiskCenterDto>> {
                Body = result,
                Code = Response.StatusCode
            };

            return Ok(response);
        }
    }
}

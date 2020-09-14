using AutoMapper;
using CreditManagementSystem.Client.Model.Credit;
using CreditManagementSystem.Client.Model.CreditStatus;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Common.Response;
using CreditManagementSystem.Domain.ComandCredit;
using CreditManagementSystem.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CreditManagementSystem.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    public class CreditController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly ICreditService _creditService;
        private readonly IMapper _mapper;

        public CreditController(
            ICommandDispatcher dispatcher,
            ICreditService creditService,
            IMapper mapper)
        {
            this._dispatcher = dispatcher;
            this._creditService = creditService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all all Credit.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<CreditCreateResultDto>>>> Get()
        {
            var result = await this._creditService.GetAll<CreditCreateResultDto>();

            var response = new Response<IEnumerable<CreditCreateResultDto>> {
                Body = result,
                Code = Response.StatusCode
            };

            return Ok(response);
        }

        /// <summary>
        /// Insert Credit.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<IEnumerable<CreditCreateResultDto>>>> Post([FromBody] CreditCreateDto dto)
        {
            var command = _mapper.Map(dto, new CreditCreateCommand());

            var response = await _dispatcher.DispatchAsync(command);

            return Ok(response);
        }
    }
}

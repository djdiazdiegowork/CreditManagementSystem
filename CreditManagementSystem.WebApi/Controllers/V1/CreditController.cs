using AutoMapper;
using CreditManagementSystem.Client.Model.Credit;
using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Response;
using CreditManagementSystem.Domain.CommandCredit;
using CreditManagementSystem.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<Response<IEnumerable<CreditResultDto>>>> Get()
        {
            var result = await this._creditService.GetAll<CreditResultDto>();

            var response = new Response<IEnumerable<CreditResultDto>>
            {
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
        public async Task<ActionResult<Response<IEnumerable<CreditResultDto>>>> Post([FromBody] CreditCreateDto dto)
        {
            var command = _mapper.Map(dto, new CreditCreateCommand());

            var response = await _dispatcher.DispatchAsync(command);

            return Ok(response);
        }

        /// <summary>
        /// Update Credit.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response<IEnumerable<CreditResultDto>>>> Put([FromBody] CreditUpdateDto dto)
        {
            var command = _mapper.Map(dto, new CreditUpdateCommand());

            var response = await _dispatcher.DispatchAsync(command);

            return Ok(response);
        }

        /// <summary>
        /// Delete Credit.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Response<Guid>>> Delete([FromBody] CreditDeleteDto dto)
        {
            var command = _mapper.Map(dto, new CreditDeleteCommand());

            var response = await _dispatcher.DispatchAsync(command);

            return Ok(response);
        }
    }
}

﻿using CreditManagementSystem.Common.Responses;
using CreditManagementSystem.Domain.Services;
using CreditManagementSystem.WebApi.Models.CreditStatus;
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
    public sealed class CreditStatusController : ControllerBase
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
        public async Task<ActionResult<Response<IEnumerable<CreditStatusDto>>>> Get()
        {
            var result = await this._creditStatusService.GetAll<CreditStatusDto>();

            var response = new Response<IEnumerable<CreditStatusDto>>(Response.StatusCode, result, null);

            return Ok(response);
        }
    }
}

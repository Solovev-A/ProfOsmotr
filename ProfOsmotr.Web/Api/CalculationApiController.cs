using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/calculation")]
    public class CalculationApiController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICalculationService calculationService;
        private readonly IAccessService accessService;

        public CalculationApiController(IMapper mapper, ICalculationService calculationService, IAccessService accessService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        [HttpPost]
        [Route("create")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Create([FromBody] CreateCalculationQuery resource)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId) || !accessService.TryGetUserId(out int userId))
                return Forbid();

            var request = mapper.Map<CreateCalculationRequest>(resource);
            request.ClinicId = clinicId;
            request.UserId = userId;
            var response = await calculationService.MakeCalculation(request);
            if (!response.Succeed)
            {
                return BadRequest(new ErrorResource(response.Message));
            }
            var calculationResource = mapper.Map<CalculationResource>(response.Result);
            return Ok(calculationResource);
        }

        [HttpPost]
        [Route("update")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Update([FromBody] UpdateCalculationQuery resource)
        {
            if (!accessService.TryGetUserId(out int userId))
                return Forbid();

            var accessResult = await accessService.CanAccessCalculationAsync(resource.CalculationId);
            if (!accessResult.AccessGranted)
                return Forbid();

            var request = mapper.Map<UpdateCalculationRequest>(resource);
            request.CreatorId = userId;

            var response = await calculationService.UpdateCalculationAsync(request);
            if (!response.Succeed)
            {
                return BadRequest(new ErrorResource(response.Message));
            }
            var calculationResource = mapper.Map<CalculationResource>(response.Result);
            return Ok(calculationResource);
        }
    }
}
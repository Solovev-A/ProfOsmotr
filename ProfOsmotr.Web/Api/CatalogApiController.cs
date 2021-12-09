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
    [Route("api/catalog")]
    public class CatalogApiController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICatalogService catalogService;
        private readonly IAccessService accessService;

        public CatalogApiController(IMapper mapper, ICatalogService catalogService, IAccessService accessService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        [HttpPost("update")]
        [ModelStateValidationFilter]
        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> Update([FromBody] UpdateCatalogItemQuery updateResource)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<SaveServiceRequest>(updateResource);
            request.ClinicId = clinicId;

            var response = await catalogService.UpdateActualAsync(request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var resource = mapper.Map<CatalogItemResource>(response.Result);
            return Ok(resource);
        }
    }
}
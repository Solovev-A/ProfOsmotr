using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IAccessService accessService;
        private readonly ICatalogService catalogService;
        private readonly IMapper mapper;

        public CatalogController(IAccessService accessService, IMapper mapper, ICatalogService catalogService)
        {
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> List()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var catalogResponse = await catalogService.GetCatalogAsync(clinicId);
            if (!catalogResponse.Succeed)
                return BadRequest(catalogResponse.Message);

            var resources = mapper.Map<IEnumerable<CatalogItemResource>>(catalogResponse.Result)
                .OrderBy(resource => resource.OrderExaminationName);
            return View(resources);
        }
    }
}
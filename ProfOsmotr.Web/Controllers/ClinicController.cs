using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicService clinicService;
        private readonly IMapper mapper;
        private readonly IAccessService accessService;

        public ClinicController(IClinicService clinicService, IMapper mapper, IAccessService accessService)
        {
            this.clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        [AuthorizeAdministrator]
        public IActionResult List()
        {
            return View();
        }

        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> Settings()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            ClinicResponse response = await clinicService.GetClinic(clinicId);

            if (!response.Succeed)
                return BadRequest(response.Message);
            var model = mapper.Map<ClinicResource>(response.Result);

            return View(model);
        }
    }
}
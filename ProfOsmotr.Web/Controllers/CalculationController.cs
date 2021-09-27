using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Models;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Controllers
{
    public class CalculationController : Controller
    {
        private readonly IAccessService accessService;
        private readonly ICalculationService calculationService;
        private readonly IMapper mapper;

        public CalculationController(IMapper mapper, ICalculationService calculationService, IAccessService accessService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        public IActionResult Company()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await CanAccessCalculation(id))
                return Forbid();

            var response = await calculationService.GetCalculation(id);
            if (!response.Succeed)
            {
                return BadRequest();
            }

            var calculationResource = mapper.Map<CalculationResource>(response.Result);
            return View(calculationResource);
        }

        public async Task<IActionResult> List([FromQuery] PaginationQueryResource queryResource)
        {
            if (queryResource.Page < 1)
                ModelState.AddModelError("page", "Страница должна быть положительным числом");
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(ModelState.GetErrorMessages()));

            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<CalculationsPaginationQuery>(queryResource);
            request.ClinicId = clinicId;

            PaginatedResult<Calculation> result = await calculationService.ListAsync(request);

            var resource = mapper.Map<PaginatedResource<CalculationGeneralDataResource>>(result);
            return View(resource);
        }

        public async Task<IActionResult> Result(int id)
        {
            if (!await CanAccessCalculation(id))
                return Forbid();

            var response = await calculationService.GetCalculation(id);
            if (!response.Succeed)
            {
                return BadRequest();
            }

            var calculationResource = mapper.Map<CalculationResource>(response.Result);
            return View(calculationResource);
        }

        public IActionResult Single()
        {
            return View();
        }

        private async Task<bool> CanAccessCalculation(int id)
        {
            var accessResult = await accessService.CanAccessCalculationAsync(id);
            return accessResult.AccessGranted;
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/examinations/periodic")]
    public class PeriodicMedicalExaminationApiController : ControllerBase
    {
        private readonly IExaminationsService examinationsService;
        private readonly IAccessService accessService;
        private readonly IMapper mapper;
        private readonly IQueryHandler queryHandler;

        public PeriodicMedicalExaminationApiController(
            IExaminationsService examinationsService,
            IAccessService accessService,
            IMapper mapper,
            IQueryHandler queryHandler)
        {
            this.examinationsService = examinationsService ?? throw new ArgumentNullException(nameof(examinationsService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<ExecuteQueryBaseRequest>(query);
            request.ClinicId = clinicId;

            return await queryHandler.HandleQuery<QueryResult<PeriodicMedicalExamination>,
                PagedResource<PeriodicMedicalExaminationListItemResource>>(
                async () => await examinationsService.ListPeriodicMedicalExaminationsAsync(request));
        }

        [HttpGet("actual")]
        public async Task<IActionResult> Get()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            return await queryHandler.HandleQuery<QueryResult<PeriodicMedicalExamination>,
                PagedResource<PeriodicMedicalExaminationListItemResource>>(
                async () => await examinationsService.ListActualPeriodicMedicalExaminationsAsync(clinicId));
        }
    }
}

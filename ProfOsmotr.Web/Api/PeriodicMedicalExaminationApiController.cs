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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await queryHandler.HandleQuery<PeriodicMedicalExamination, PeriodicMedicalExaminationResource>(
                async () => await examinationsService.GetPeriodicMedicalExaminationAsync(id),
                async () => await accessService.CanAccessPeriodicExaminationAsync(id));
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

        [HttpPatch("{id}")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchPeriodicExaminationQuery query)
        {
            if (!accessService.TryGetUserId(out int userId))
                return Forbid();

            var request = new UpdatePeriodicExaminationRequest()
            {
                EditorId = userId,
                ExaminationId = id,
                Query = query
            };

            return await queryHandler.HandleQuery<PeriodicMedicalExamination>(
                async () => await examinationsService.UpdatePeriodicExaminationAsync(request),
                async () => await accessService.CanAccessPeriodicExaminationAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await queryHandler.HandleQuery<PeriodicMedicalExamination>(
                async () => await examinationsService.DeletePeriodicExaminationAsync(id),
                async () => await accessService.CanAccessPeriodicExaminationAsync(id));
        }

        [HttpPost("{examinationId}/checkup-statuses")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> CreateCheckupStatus(int examinationId, [FromBody] CreateContingentCheckupStatusQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            if (!accessService.TryGetUserId(out int userId))
                return Forbid();

            var request = mapper.Map<CreateContingentCheckupStatusRequest>(query);
            request.ClinicId = clinicId;
            request.ExaminationId = examinationId;
            request.CreatorId = userId;

            var accessChecks = new List<Func<Task<AccessResult>>>();
            accessChecks.Add(async () => await accessService.CanAccessPeriodicExaminationAsync(examinationId));
            accessChecks.Add(async () => await accessService.CanAccessPatientAsync(query.PatientId));

            if (query.EmployerDepartmentId.HasValue)
                accessChecks.Add(async () => await accessService.CanAccessEmployerDepartmentAsync(query.EmployerDepartmentId.Value));

            return await queryHandler.HandleQuery<ContingentCheckupStatus>(
                async () => await examinationsService.CreateContingentCheckupStatus(request),
                accessChecks.ToArray());
        }

        [HttpGet("checkup-statuses/{id}")]
        public async Task<IActionResult> GetCheckupStatus(int id)
        {
            return await queryHandler.HandleQuery<ContingentCheckupStatus, ContingentCheckupStatusResource>(
                async () => await examinationsService.GetContingentCheckupStatus(id),
                async () => await accessService.CanAccessContingentCheckupStatus(id));
        }
    }
}

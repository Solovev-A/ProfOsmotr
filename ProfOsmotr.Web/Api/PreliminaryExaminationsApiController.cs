using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/examinations/preliminary")]
    public class PreliminaryExaminationsApiController : ControllerBase
    {
        private readonly IExaminationsService examinationsService;
        private readonly IAccessService accessService;
        private readonly IMapper mapper;
        private readonly IQueryHandler queryHandler;

        public PreliminaryExaminationsApiController(
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
            return await queryHandler.HandleQuery<PreliminaryMedicalExamination, PreliminaryMedicalExaminationResource>(
                async () => await examinationsService.GetPreliminaryMedicalExaminationAsync(id),
                async () => await accessService.CanAccessPreliminaryExaminationAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PreliminaryExaminationSearchPaginationQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<ExecutePreliminaryExaminationsQueryRequest>(query);
            request.ClinicId = clinicId;

            Func<Task<BaseResponse<QueryResult<PreliminaryMedicalExamination>>>> serviceFunc
                = async () => await examinationsService.ListPreliminaryMedicalExaminationsAsync(request);

            if (query.EmployerId.HasValue)
            {
                return await queryHandler.HandleQuery<QueryResult<PreliminaryMedicalExamination>,
                    PagedResource<EmployerPreliminaryMedicalExaminationResource>>(serviceFunc);
            }

            return await queryHandler.HandleQuery<QueryResult<PreliminaryMedicalExamination>,
                PagedResource<PreliminaryMedicalExaminationsListItemResource>>(serviceFunc);
        }

        [HttpGet("actual")]
        public async Task<IActionResult> Get()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            return await queryHandler.HandleQuery<QueryResult<PreliminaryMedicalExamination>,
                PagedResource<PreliminaryMedicalExaminationsListItemResource>>(
                async () => await examinationsService.ListActualPreliminaryMedicalExaminationsAsync(clinicId));
        }

        [HttpGet("{id}/report")]
        public async Task<IActionResult> GetMedicalReport(int id)
        {
            return await queryHandler.HandleQuery<BaseFileResult>(
                async () => await examinationsService.GetPreliminaryExaminationMedicalReportAsync(id),
                (res) => File(res.Bytes, res.ContentType, res.FileName),
                async () => await accessService.CanAccessPreliminaryExaminationAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePreliminaryExaminationQuery query)
        {
            if (!accessService.TryGetUserId(out int userId))
                return Forbid();

            var request = new CreatePreliminaryMedicalExaminationRequest()
            {
                PatientId = query.PatientId,
                CreatorId = userId
            };

            return await queryHandler.HandleQuery<PreliminaryMedicalExamination, CreatedExaminationResource>(
                async () => await examinationsService.CreatePreliminaryMedicalExaminationAsync(request),
                async () => await accessService.CanAccessPatientAsync(query.PatientId));
        }

        [HttpPatch("{id}")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchPreliminaryExaminationQuery query)
        {
            if (!accessService.TryGetUserId(out int userId))
                return Forbid();

            var request = new UpdatePreliminaryExaminationRequest()
            {
                EditorId = userId,
                ExaminationId = id,
                Query = query
            };

            var accessChecks = new List<Func<Task<AccessResult>>>
            {
                async () => await accessService.CanAccessPreliminaryExaminationAsync(id)
            };
            if (query.IsFieldPresent(nameof(query.EmployerId)) && query.EmployerId.HasValue)
            {
                accessChecks.Add(async () => await accessService.CanAccessEmployerAsync(query.EmployerId.Value));
            }

            return await queryHandler.HandleQuery<PreliminaryMedicalExamination>(
                async () => await examinationsService.UpdatePreliminaryExaminationAsync(request),
                accessChecks.ToArray());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await queryHandler.HandleQuery<PreliminaryMedicalExamination>(
                async () => await examinationsService.DeletePreliminaryExaminationAsync(id),
                async () => await accessService.CanAccessPreliminaryExaminationAsync(id));
        }

        [HttpGet("journal")]
        public async Task<IActionResult> GetJournal(ExaminationJournalQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<ExecuteExaminationsJournalQueryRequest>(query);
            request.ClinicId = clinicId;

            return await queryHandler.HandleQuery<QueryResult<PreliminaryMedicalExamination>,
                PagedResource<PreliminaryMedicalExaminationsJournalItemResource>>(
                async () => await examinationsService.GetPreliminaryMedicalExaminationsJournalAsync(request));
        }
    }
}

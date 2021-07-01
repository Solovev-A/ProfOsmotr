﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
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
                async () => await accessService.CanAccessPrealiminaryExaminationAsync(id));
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

            return await queryHandler.HandleQuery<PreliminaryMedicalExamination, CreatedPreliminaryExaminationResource>(
                async () => await examinationsService.CreatePreliminaryMedicalExaminationAsync(request),
                async () => await accessService.CanAccessPatientAsync(query.PatientId));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id)
        {
            // TODO
            return StatusCode(405); // 405 Method Not Allowed
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await queryHandler.HandleQuery<PreliminaryMedicalExamination>(
                async () => await examinationsService.DeleteExaminationAsync(id),
                async () => await accessService.CanAccessPrealiminaryExaminationAsync(id));
        }
    }
}

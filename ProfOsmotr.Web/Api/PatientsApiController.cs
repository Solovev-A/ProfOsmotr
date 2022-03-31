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
    [Route("api/patients")]
    public class PatientsApiController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IAccessService accessService;
        private readonly IMapper mapper;
        private readonly IQueryHandler queryHandler;

        public PatientsApiController(IPatientService patientService, IAccessService accessService, IMapper mapper, IQueryHandler queryHandler)
        {
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Get([FromQuery] SearchPatientQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            if (query.EmployerId.HasValue)
            {
                var request = mapper.Map<FindPatientWithSuggestionsRequest>(query);
                request.ClinicId = clinicId;

                return await queryHandler.HandleQuery<PatientSearchResult, PatientSmartSearchResultResource>(
                    async () => await patientService.FindPatientWithSuggestions(request),
                    async () => await accessService.CanAccessEmployerAsync(request.EmployerId));
            }
            else
            {
                var request = mapper.Map<ListPatientsRequest>(query);
                request.ClinicId = clinicId;

                return await queryHandler.HandleQuery<QueryResult<Patient>, PagedResource<PatientsListItemResource>>(
                    async () => await patientService.ListPatientsAsync(request));
            }
        }

        [HttpGet("actual")]
        public async Task<IActionResult> Get()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            return await queryHandler.HandleQuery<QueryResult<Patient>, PagedResource<PatientsListItemResource>>(
                async () => await patientService.ListActualPatientsAsync(clinicId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await queryHandler.HandleQuery<Patient, PatientResource>(
                async () => await patientService.GetPatientAsync(id),
                async () => await accessService.CanAccessPatientAsync(id));
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Post([FromBody] CreatePatientQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<CreatePatientRequest>(query);
            request.ClinicId = clinicId;

            return await queryHandler.HandleQuery<Patient, PatientsListItemResource>(
                async () => await patientService.CreatePatient(request));
        }

        [HttpPatch("{id}")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchPatientQuery query)
        {
            var request = new UpdatePatientRequest()
            {
                PatientId = id,
                Query = query
            };

            return await queryHandler.HandleQuery<Patient>(
                async () => await patientService.UpdatePatientAsync(request),
                async () => await accessService.CanAccessPatientAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await queryHandler.HandleQuery<Patient>(
                async () => await patientService.DeletePatientAsync(id),
                async () => await accessService.CanAccessPatientAsync(id));
        }
    }
}
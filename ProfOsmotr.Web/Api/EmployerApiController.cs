using AutoMapper;
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
    [Route("api/employers")]
    public class EmployerApiController : ControllerBase
    {
        private readonly IEmployerService employerService;
        private readonly IAccessService accessService;
        private readonly IMapper mapper;
        private readonly IQueryHandler queryHandler;

        public EmployerApiController(IEmployerService employerService, IAccessService accessService, IMapper mapper, IQueryHandler queryHandler)
        {
            this.employerService = employerService ?? throw new ArgumentNullException(nameof(employerService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await queryHandler.HandleQuery<Employer, EmployerResource>(
                async () => await employerService.GetEmployerAsync(id),
                async () => await accessService.CanAccessEmployerAsync(id));
        }

        [HttpGet("actual")]
        public async Task<IActionResult> Get()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            return await queryHandler.HandleQuery<QueryResult<Employer>, PagedResource<EmployerListItemResource>>(
                async () => await employerService.ListActualEmployersAsync(clinicId));
        }

        [HttpGet]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<ExecuteQueryBaseRequest>(query);
            request.ClinicId = clinicId;

            return await queryHandler.HandleQuery<QueryResult<Employer>, PagedResource<EmployerListItemResource>>(
                async () => await employerService.ListEmployersAsync(request));
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Post([FromBody] CreateEmployerQuery query)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = mapper.Map<CreateEmployerRequest>(query);
            request.ClinicId = clinicId;

            return await queryHandler.HandleQuery<Employer, EmployerListItemResource>(
                async () => await employerService.CreateEmployerAsync(request));
        }

        [HttpPatch("{id}")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchEmployerQuery query)
        {
            var request = new UpdateEmployerRequest()
            {
                EmployerId = id,
                Query = query
            };

            return await queryHandler.HandleQuery<Employer>(
                async () => await employerService.UpdateEmployerAsync(request),
                async () => await accessService.CanAccessEmployerAsync(id));
        }

        [HttpPost("{employerId}/departments")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> CreateDepartment(int employerId, [FromBody] CreateEmployerDepartmentQuery query)
        {
            var request = mapper.Map<CreateEmployerDepartmentRequest>(query);
            request.ParentId = employerId;

            return await queryHandler.HandleQuery<EmployerDepartment, EmployerDepartmentResource>(
                async () => await employerService.CreateEmployerDepartmentAsync(request),
                async () => await accessService.CanAccessEmployerAsync(employerId));
        }

        [HttpPatch("/api/departments/{id}")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] PatchEmployerDepartmentQuery query)
        {
            var request = new UpdateEmployerDepartmentRequest()
            {
                EmployerDepartmentId = id,
                Query = query
            };

            return await queryHandler.HandleQuery<EmployerDepartment>(
                async () => await employerService.UpdateEmployerDepartmentAsync(request),
                async () => await accessService.CanAccessEmployerDepartmentAsync(id));
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Models.DataTables;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/clinic")]
    public class ClinicApiController : Controller
    {
        private readonly IMapper mapper;
        private readonly IClinicService clinicService;
        private readonly IAccessService accessService;

        public ClinicApiController(IMapper mapper, IClinicService clinicService, IAccessService accessService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        [HttpPost]
        [Route("addRegisterRequest")]
        [ModelStateValidationFilter]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRegisterRequest([FromBody] CreateRegisterRequestResource resource)
        {
            var request = mapper.Map<RegisterDataRequest>(resource);

            var response = await clinicService.AddRegisterRequest(request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var result = mapper.Map<RegisterRequestResource>(response.Result);

            return Ok(result);
        }

        [HttpPost]
        [Route("list")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ListClincs([FromBody] DataTablesParameters parameters)
        {
            DataTablesQuery query;
            try
            {
                query = DataTablesHelper.GetQuery(typeof(ClinicResource), parameters);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResource(ex.Message));
            }
            QueryResponse<Clinic> response = await clinicService.ListClinics(query.Start,
                                                           query.Length,
                                                           query.Search,
                                                           query.OrderingSelector,
                                                           query.Descending);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            var resource = mapper.Map<IEnumerable<ClinicResource>>(response.Result.Data);
            var result = new DataTablesResult<ClinicResource>()
            {
                Data = resource,
                Draw = parameters.Draw,
                RecordsFiltered = response.Result.TotalItems,
                RecordsTotal = response.Result.TotalItems
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("newRequests")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ListNewRegisterRequests([FromBody] DataTablesParameters parameters)
        {
            return await ListRegisterRequests(parameters, GetNewRegisterRequests);
        }

        [HttpPost]
        [Route("processedRequests")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ListProcessedRegisterRequests([FromBody] DataTablesParameters parameters)
        {
            return await ListRegisterRequests(parameters, GetProcessedRegisterRequests);
        }

        [HttpPost]
        [Route("manageRequest")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ManageRegisterRequest([FromBody] ManageRegisterRequestResource resource)
        {
            if (resource.Approved)
            {
                var response = await clinicService.AddClinic(resource.Id);
                if (!response.Succeed)
                    return BadRequest(new ErrorResource(response.Message));

                var result = mapper.Map<ClinicResource>(response.Result);
                return Ok(result);
            }
            else
            {
                var response = await clinicService.RejectRegisterRequest(resource.Id);
                if (!response.Succeed)
                    return BadRequest(new ErrorResource(response.Message));

                var result = mapper.Map<RegisterRequestResource>(response.Result);
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("manageClinic")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ManageClinic([FromBody] ManageClinicResource resource)
        {
            if (!accessService.TryGetUserClinicId(out int userClinicId))
                return Forbid();
            if (userClinicId == resource.Id)
                return BadRequest(new ErrorResource("Недопустимая операция в отношении своей собственной МО"));
            
            var request = mapper.Map<ManageClinicRequest>(resource);
            var response = await clinicService.ManageClinic(request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var result = mapper.Map<ClinicResource>(response.Result);
            return Ok(result);
        }

        [HttpPost]
        [Route("updateDetails")]
        [ModelStateValidationFilter]
        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateClinicDetailsResource resource)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();
            var request = mapper.Map<UpdateClinicDetailsRequest>(resource);
            request.ClinicId = clinicId;
            ClinicResponse response = await clinicService.UpdateDetails(request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var result = mapper.Map<ClinicResource>(response.Result);

            return Ok(result);
        }

        private async Task<IActionResult> ListRegisterRequests(DataTablesParameters parameters,
                                                               Func<DataTablesQuery, Task<QueryResponse<ClinicRegisterRequest>>> listGetter)
        {
            DataTablesQuery query = null;
            try
            {
                query = DataTablesHelper.GetQuery(typeof(RegisterRequestResource), parameters);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResource(ex.Message));
            }
            QueryResponse<ClinicRegisterRequest> response = await listGetter(query);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            var resource = mapper.Map<IEnumerable<RegisterRequestResource>>(response.Result.Data);
            var result = new DataTablesResult<RegisterRequestResource>()
            {
                Data = resource,
                Draw = parameters.Draw,
                RecordsFiltered = response.Result.TotalItems,
                RecordsTotal = response.Result.TotalItems
            };
            return Ok(result);
        }

        private async Task<QueryResponse<ClinicRegisterRequest>> GetNewRegisterRequests(DataTablesQuery query)
        {
            return await clinicService.ListNewRegisterRequests(
                    query.Start,
                    query.Length,
                    query.Search,
                    query.OrderingSelector,
                    query.Descending);
        }

        private async Task<QueryResponse<ClinicRegisterRequest>> GetProcessedRegisterRequests(DataTablesQuery query)
        {
            return await clinicService.ListProcessedRegisterRequests(
                    query.Start,
                    query.Length,
                    query.Search,
                    query.OrderingSelector,
                    query.Descending);
        }
    }
}
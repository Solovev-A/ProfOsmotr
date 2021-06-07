using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Infrastructure.Extensions;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Models.DataTables;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/user")]
    public class UserApiController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IAccessService accessService;

        public UserApiController(IUserService userService, IMapper mapper, IAuthService authService, IAccessService accessService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        }

        [HttpPost]
        [Route("create")]
        [ModelStateValidationFilter]
        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> Create([FromBody] CreateUserResource resource)
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();
            if (!CanSetRole(resource.RoleId))
                return BadRequest(new ErrorResource("Недопустимое значение roleId"));

            var request = mapper.Map<CreateUserRequest>(resource);
            var response = await userService.AddUserAsync(clinicId, request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            var result = mapper.Map<UsersListItem>(response.Result);
            return Ok(result);
        }

        [HttpPost]
        [Route("list")]
        [ModelStateValidationFilter]
        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> List([FromBody] DataTablesParameters parameters)
        {
            if (User.IsInRole(RoleId.Administrator.ToClaimValue()))
            {
                var query = DataTablesHelper.GetQuery(typeof(UsersListGlobalItem), parameters);
                QueryResponse<User> response = await userService.ListAllUsers(query.Start,
                                                                              query.Length,
                                                                              query.Search,
                                                                              query.OrderingSelector,
                                                                              query.Descending);
                return ListUsers<UsersListGlobalItem>(parameters, response);
            }
            else
            {
                if (!accessService.TryGetUserClinicId(out int clinicId))
                    return Forbid();

                var query = DataTablesHelper.GetQuery(typeof(UsersListItem), parameters);
                QueryResponse<User> response = await userService.ListClinicUsers(clinicId,
                                                                                 query.Start,
                                                                                 query.Length,
                                                                                 query.Search,
                                                                                 query.OrderingSelector,
                                                                                 query.Descending);
                return ListUsers<UsersListItem>(parameters, response);
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginResource resource)
        {
            var authResult = await authService.Authenticate(resource);
            return Ok(authResult);
        }

        [HttpPost]
        [Route("update/{id}")]
        [ModelStateValidationFilter]
        [AuthorizeAdministratorAndClinicModerator]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserResource resource)
        {
            if (resource.RoleId.HasValue && !CanSetRole(resource.RoleId.Value, id))
                return BadRequest(new ErrorResource("Недопустимое значение roleId"));

            AccessResult accessResult = await accessService.CanManageUserAsync(id);
            if (!accessResult.AccessGranted)
                return Forbid();

            var request = mapper.Map<UpdateUserRequest>(resource);

            var response = await userService.UpdateUserAsync(id, request);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            var result = mapper.Map<UsersListItem>(response.Result);
            return Ok(result);
        }

        private bool CanSetRole(int id, int? userId = null)
        {
            if (!Enum.IsDefined(typeof(RoleId), id))
                return false;
            RoleId roleToSet = (RoleId)id;

            if (userId.HasValue && User.FindFirstValue(AuthenticationService.UserIdClaimType) == userId.ToString()
                && !User.IsInRole(roleToSet.ToClaimValue()))
                return false;

            if (User.IsInRole(RoleId.Administrator.ToClaimValue()))
                return true;

            if (User.IsInRole(RoleId.ClinicModerator.ToClaimValue()))              
                return roleToSet != RoleId.Administrator;

            return false;
        }

        private IActionResult ListUsers<TModel>(DataTablesParameters parameters, QueryResponse<User> response)
        {
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            IEnumerable<TModel> resource = mapper.Map<IEnumerable<TModel>>(response.Result.Items);
            DataTablesResult<TModel> result = new DataTablesResult<TModel>()
            {
                Data = resource,
                Draw = parameters.Draw,
                RecordsFiltered = response.Result.TotalCount,
                RecordsTotal = response.Result.TotalCount
            };
            return Ok(result);
        }
    }
}
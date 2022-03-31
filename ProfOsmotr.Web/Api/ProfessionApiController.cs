using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Models;
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
    [Route("api/professions")]
    public class ProfessionApiController : Controller
    {
        private readonly IAccessService accessService;
        private readonly IQueryHandler queryHandler;
        private readonly IProfessionService professionService;
        private readonly IMapper mapper;

        public ProfessionApiController(IAccessService accessService, IQueryHandler queryHandler, IProfessionService professionService, IMapper mapper)
        {
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
            this.professionService = professionService ?? throw new ArgumentNullException(nameof(professionService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchProfessionQuery query)
        {
            var request = mapper.Map<FindProfessionRequest>(query);

            Func<Task<AccessResult>> accessCheck = null;
            if (request.EmployerId.HasValue)
            {
                accessCheck = async () => await accessService.CanAccessEmployerAsync(request.EmployerId.Value);
            }

            return await queryHandler.HandleQuery<ProfessionSearchResult, ProfessionSearchResultResource>(
                async () => await professionService.FindProfessionWithSuggestions(request),
                accessCheck);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Post([FromBody] CreateProfessonQuery query)
        {
            var request = mapper.Map<CreateProfessionRequest>(query);

            return await queryHandler.HandleQuery<Profession, ProfessionResource>(
                async () => await professionService.CreateProfession(request));
        }
    }
}

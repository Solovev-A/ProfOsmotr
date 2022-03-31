using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Models;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/statistics")]
    public class StatisticsApiController : Controller
    {
        private readonly IExaminationsService examinationsService;
        private readonly IAccessService accessService;
        private readonly IQueryHandler queryHandler;

        public StatisticsApiController(IExaminationsService examinationsService,
                                       IAccessService accessService,
                                       IQueryHandler queryHandler)
        {
            this.examinationsService = examinationsService ?? throw new ArgumentNullException(nameof(examinationsService));
            this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet("examinations")]
        public async Task<IActionResult> GetExaminationsStatistics()
        {
            if (!accessService.TryGetUserClinicId(out int clinicId))
                return Forbid();

            var request = new CalculateStatisticsRequest()
            {
                ClinicId = clinicId
            };

            return await queryHandler.HandleQuery<IEnumerable<ExaminationsStatisticsData>,
                                                  IEnumerable<ExaminationsStatisticsDataResource>>(
                async () => await examinationsService.CalculateStatistics(request));
        }
    }
}
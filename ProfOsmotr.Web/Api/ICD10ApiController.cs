using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Models;
using ProfOsmotr.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    [Route("api/icd10")]
    public class ICD10ApiController : ControllerBase
    {
        private readonly IICD10Service icd10Service;
        private readonly IQueryHandler queryHandler;

        public ICD10ApiController(IICD10Service icd10Service, IQueryHandler queryHandler)
        {
            this.icd10Service = icd10Service ?? throw new ArgumentNullException(nameof(icd10Service));
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await queryHandler.HandleQuery<IEnumerable<ICD10Chapter>, IEnumerable<ICD10ChapterResource>>(
                async () => await icd10Service.ListChaptersAsync());
        }
    }
}
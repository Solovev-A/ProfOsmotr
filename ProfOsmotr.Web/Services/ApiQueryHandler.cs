using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.Web.Models;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    public class ApiQueryHandler : IQueryHandler
    {
        private readonly IMapper mapper;

        public ApiQueryHandler(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> HandleQuery<TServiceResult, TResource>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<Task<AccessResult>> accessCheck = null)
        {
            return await ProcessQuery(serviceFunc, accessCheck, MapServiceResultToResourceAndReturnOk<TServiceResult, TResource>);
        }

        public async Task<IActionResult> HandleQuery<TServiceResult>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<Task<AccessResult>> accessCheck = null)
        {
            return await ProcessQuery(serviceFunc, accessCheck, (res) => new OkResult());
        }

        protected async Task<IActionResult> ProcessQuery<TServiceResult>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<Task<AccessResult>> accessCheck,
            Func<TServiceResult, IActionResult> onSuccessResponse)
        {
            if (accessCheck != null)
            {
                var accessResult = await accessCheck();
                if (!accessResult.AccessGranted)
                {
                    return new ForbidResult();
                }
            }

            BaseResponse<TServiceResult> serviceResponse = await serviceFunc();

            if (!serviceResponse.Succeed)
            {
                var error = new ErrorResource(serviceResponse.Message);
                return new BadRequestObjectResult(error);
            }

            return onSuccessResponse(serviceResponse.Result);
        }

        private IActionResult MapServiceResultToResourceAndReturnOk<TServiceResult, TResource>(TServiceResult serviceResult)
        {
            TResource resource = mapper.Map<TResource>(serviceResult);
            return new OkObjectResult(resource);
        }
    }
}
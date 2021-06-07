using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.Web.Models;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    public interface IQueryHandler
    {
        Task<IActionResult> HandleQuery<TServiceResult, TResource>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<Task<AccessResult>> accessCheck = null);

        Task<IActionResult> HandleQuery<TServiceResult>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<Task<AccessResult>> accessCheck = null);
    }
}
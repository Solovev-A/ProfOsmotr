using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.Web.Models;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    /// <summary>
    /// Представляет абстракцию для обработчика запроса
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// Обрабатывает запрос с использованием сервисов бизнес-логики
        /// </summary>
        /// <typeparam name="TServiceResult">Тип результата сервиса бизнес-логики</typeparam>
        /// <typeparam name="TResource">Тип ресурса, который может быть использован в ответе</typeparam>
        /// <param name="serviceFunc">Метод сервиса бизнес-логики, обрабатывающий запрос</param>
        /// <param name="accessChecks">Проверки доступа</param>
        /// <returns></returns>
        Task<IActionResult> HandleQuery<TServiceResult, TResource>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            params Func<Task<AccessResult>>[] accessChecks);

        /// <summary>
        /// Обрабатывает запрос с использованием сервисов бизнес-логики. Не предусматривает ответ с json нагрузкой
        /// </summary>
        /// <typeparam name="TServiceResult">Тип результата сервиса бизнес-логики</typeparam>
        /// <param name="serviceFunc">Метод сервиса бизнес-логики, обрабатывающий запрос</param>
        /// <param name="accessChecks">Проверки доступа</param>
        /// <returns></returns>
        Task<IActionResult> HandleQuery<TServiceResult>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            params Func<Task<AccessResult>>[] accessChecks);

        /// <summary>
        /// Обрабатывает запрос с использованием сервисов бизнес-логики. Возможно переопределение логики создания результата
        /// </summary>
        /// <typeparam name="TServiceResult"></typeparam>
        /// <param name="serviceFunc">Метод сервиса бизнес-логики, обрабатывающий запрос</param>
        /// <param name="actionResultCreator">Функция, производящая создание результата</param>
        /// <param name="accessChecks">Проверки доступа</param>
        /// <returns></returns>
        Task<IActionResult> HandleQuery<TServiceResult>(
            Func<Task<BaseResponse<TServiceResult>>> serviceFunc,
            Func<TServiceResult, IActionResult> actionResultCreator,
            params Func<Task<AccessResult>>[] accessChecks);
    }
}
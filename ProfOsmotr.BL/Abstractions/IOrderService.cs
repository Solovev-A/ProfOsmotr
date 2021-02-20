﻿using ProfOsmotr.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса приказа, регулирующего порядок медицинского осмотра
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Добавляет медицинское обследование
        /// </summary>
        /// <param name="request">Исходные данные для сохранения обследования</param>
        /// <returns></returns>
        Task<OrderExaminationResponse> AddExaminationAsync(SaveOrderExaminationRequest request);

        /// <summary>
        /// Создает показатель результата для обследования с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор обследования, для которого создается показатель результата</param>
        /// <param name="request">Исходные данные для сохранения показателя результата</param>
        /// <returns></returns>
        Task<ExaminationResultIndexResponse> AddIndexAsync(int examinationId, SaveExaminationResultIndexRequest request);

        /// <summary>
        /// Добавляет новый пункт приказа
        /// </summary>
        /// <param name="request">Исходные данные для добавления пункта приказа</param>
        /// <returns></returns>
        Task<OrderItemResponse> AddItemAsync(AddOrderItemRequest request);

        /// <summary>
        /// Удаляет показаьтель результата обследования в идентификатором <paramref name="indexId"/>
        /// </summary>
        /// <param name="indexId">Идентификатор удаляемого показателя</param>
        /// <returns></returns>
        Task<ExaminationResultIndexResponse> DeleteIndexAsync(int indexId);

        /// <summary>
        /// Удаляет пункт приказа с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор удаляемого пункта</param>
        /// <returns></returns>
        Task<OrderItemResponse> DeleteItemAsync(int id);

        /// <summary>
        /// Предоставляте все актуальные пункты приказа
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderItem>> GetAllItems();

        /// <summary>
        /// Предоставляет показатели результата обследвоания по приказу
        /// </summary>
        /// <param name="examinationId">
        /// Идентификатор обследования по приказу, для которого необходимо получить показатели результата
        /// </param>
        /// <returns></returns>
        Task<ExaminationResultIndexesResponse> GetExaminationResultIndexes(int examinationId);

        /// <summary>
        /// Предоставляет все обследования по приказу, включая подробное описание услуг по умолчанию
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetExaminationsAsync();

        /// <summary>
        /// Предоставляет общие сведения обо всех обследованиях по приказу
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetExaminationsShortDataAsync();

        /// <summary>
        /// Предоставляет все актуальные пункты приказа, сгруппированные по приложениям
        /// </summary>
        /// <returns>Перечисление приложений приказа</returns>
        Task<IEnumerable<OrderAnnex>> GetOrderAsync();

        /// <summary>
        /// Предоставляет все целевые группы обследований
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TargetGroup>> GetTargetGroupsAsync();

        /// <summary>
        /// Обновляет обследование по приказу с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор обновляемого обследования</param>
        /// <param name="request">Исходные данные для сохранения обследования</param>
        /// <returns></returns>
        Task<OrderExaminationResponse> UpdateExaminationAsync(int id, SaveOrderExaminationRequest request);

        /// <summary>
        /// Обновляет показатель результата с идентификатором <paramref name="indexId"/>
        /// </summary>
        /// <param name="indexId">Идентификатор показателя результата</param>
        /// <param name="request">Исходные данные для сохранения показателя результата</param>
        /// <returns></returns>
        Task<ExaminationResultIndexResponse> UpdateIndexAsync(int indexId, SaveExaminationResultIndexRequest request);

        /// <summary>
        /// Обновляет пункт приказа
        /// </summary>
        /// <param name="request">Исходные данные для обновления пункта приказа</param>
        /// <returns></returns>
        Task<OrderItemResponse> UpdateItemAsync(UpdateOrderItemRequest request);

        /// <summary>
        /// Предоставляет пункт приказа с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор существующего пункта прикзаа</param>
        /// <returns></returns>
        internal Task<OrderItemResponse> FindItemAsync(int id);

        /// <summary>
        /// Предоставляет пункт приказа с идентификатором <paramref name="id"/>, включая актуальные
        /// услуги медицинской организации <paramref name="clinicId"/>, соответствующие
        /// обследованиям по этому пункту
        /// </summary>
        /// <param name="id">Идентификатор пункта приказа</param>
        /// <param name="clinicId">Идентификатор МО, чьи услуги должны быть включены</param>
        /// <returns></returns>
        internal Task<OrderItemResponse> FindItemWithActualServicesAsync(int id, int clinicId);

        /// <summary>
        /// Предоставляет общие обследования по приказу, необходимые для медицинского осмотра всех
        /// обследуемых, вне зависмости от условий труда
        /// </summary>
        /// <returns></returns>
        internal Task<IEnumerable<OrderItem>> GetGeneralOrderItemsAsync();
    }
}
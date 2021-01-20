using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly ICatalogService catalogService;
        private readonly IMapper mapper;
        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public OrderService(IProfUnitOfWork uow, ICatalogService catalogService, IMapper mapper)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region Methods

        public async Task<OrderExaminationResponse> AddExaminationAsync(SaveOrderExaminationRequest request)
        {
            if (request is null)
                return new OrderExaminationResponse("Запрос не может быть пустым");

            var existingExamination = await uow.OrderExaminations
                .FirstOrDefaultAsync(ex => ex.Name == request.Name);
            if (existingExamination != null)
                return new OrderExaminationResponse("Обследование с таким названием уже существует");

            OrderExamination newExamination = mapper.Map<OrderExamination>(request);

            newExamination.TargetGroup = await FindTargetGroup(request.TargetGroupId);
            if (newExamination.TargetGroup == null)
                return new OrderExaminationResponse("Группы с таким id не существует");

            try
            {
                await uow.OrderExaminations.AddAsync(newExamination);
                await catalogService.SeedAllCatalogs(newExamination);
                await uow.SaveAsync();
                return new OrderExaminationResponse(newExamination);
            }
            catch (Exception ex)
            {
                return new OrderExaminationResponse($"Во время добавления обследования произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<OrderItemResponse> AddItemAsync(AddOrderItemRequest request)
        {
            if (request is null)
                return new OrderItemResponse("Запрос не может быть пустым");
            if (!request.OrderExaminationIdentifiers.Any())
                return new OrderItemResponse("Список обследований не должен быть пустым");
            if (!Enum.IsDefined(typeof(OrderAnnexId), request.AnnexId))
                return new OrderItemResponse("Приложение с таким номером не существует");

            OrderItem existingItem = await uow.OrderItems
                .FirstOrDefaultAsync(item => item.OrderAnnexId == (OrderAnnexId)request.AnnexId
                                          && item.Key == request.Key
                                          && !item.IsDeleted);
            if (existingItem != null)
                return new OrderItemResponse("Пункт приказа с таким номером уже существует");

            var annex = await uow.OrderAnnexes.FirstOrDefaultAsync(a => a.Id == (OrderAnnexId)request.AnnexId);

            OrderItem newOrderItem = mapper.Map<AddOrderItemRequest, OrderItem>(request);
            newOrderItem.OrderAnnex = annex;

            foreach (var id in request.OrderExaminationIdentifiers)
            {
                OrderExamination orderExamination = await FindExaminationAsync(id);
                if (orderExamination == null)
                    return new OrderItemResponse($"Обследование по приказу с id #{id} не найдено");
                newOrderItem.OrderExaminations.Add(orderExamination);
            }

            try
            {
                await uow.OrderItems.AddAsync(newOrderItem);
                await uow.SaveAsync();
                return new OrderItemResponse(newOrderItem);
            }
            catch (Exception ex)
            {
                return new OrderItemResponse($"Во время добавления пункта произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<OrderItemResponse> DeleteItemAsync(int id)
        {
            var orderItem = await FindItemAsync(id);
            if (orderItem == null)
                return new OrderItemResponse("Элемент приказа не найден");

            orderItem.IsDeleted = true;

            try
            {
                await uow.SaveAsync();
                return new OrderItemResponse(orderItem);
            }
            catch (Exception ex)
            {
                return new OrderItemResponse($"Во время удаления пункта произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        async Task<OrderItemResponse> IOrderService.FindItemAsync(int id)
        {
            var result = await FindItemAsync(id);
            if (result == null)
                return new OrderItemResponse($"Пункт приказа с id #{id} не найден");
            return new OrderItemResponse(result);
        }

        public async Task<IEnumerable<OrderItem>> GetAllItems()
        {
            return await uow.OrderItems.GetActualItems();
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsAsync()
        {
            return await uow.OrderItems.GetExaminationsWithDetailsAsync();
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsShortDataAsync()
        {
            return await uow.OrderItems.GetExaminationsAsync();
        }

        public async Task<IEnumerable<OrderAnnex>> GetOrderAsync()
        {
            return await uow.OrderItems.GetOrderAsync();
        }

        public async Task<IEnumerable<TargetGroup>> GetTargetGroupsAsync()
        {
            return await uow.TargetGroups.GetAllAsync();
        }

        public async Task<OrderExaminationResponse> UpdateExaminationAsync(int id, SaveOrderExaminationRequest request)
        {
            if (request is null)
                return new OrderExaminationResponse("Запрос не может быть пустым");
            var existingExamination = await uow.OrderExaminations
                .FirstOrDefaultAsync(ex => ex.Name == request.Name);
            if (existingExamination != null && existingExamination.Id != id)
                return new OrderExaminationResponse("Обследование с таким названием уже существует");

            var examination = await FindExaminationAsync(id);
            if (examination == null)
                return new OrderExaminationResponse($"Обследование по приказу с id #{id} не найдено");

            mapper.Map(request, examination);
            examination.TargetGroup = await FindTargetGroup(request.TargetGroupId);
            if (examination.TargetGroup == null)
                return new OrderExaminationResponse("Группы с таким id не существует");

            try
            {
                await uow.SaveAsync();
                return new OrderExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new OrderExaminationResponse($"Во время обновления обследования произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<OrderItemResponse> UpdateItemAsync(UpdateOrderItemRequest request)
        {
            if (request is null)
                return new OrderItemResponse("Запрос не может быть пустым");
            if (!request.OrderExaminationIdentifiers.Any())
                return new OrderItemResponse("Список обследований не должен быть пустым");
            var existingItem = await FindItemAsync(request.Id);
            if (existingItem == null)
                return new OrderItemResponse("Элемент приказа не найден");

            existingItem.Name = request.Name;
            existingItem.OrderExaminations.Clear();

            foreach (var id in request.OrderExaminationIdentifiers)
            {
                var orderExamination = await FindExaminationAsync(id);
                if (orderExamination == null)
                    return new OrderItemResponse($"Обследование по приказу с id #{id} не найдено");

                existingItem.OrderExaminations.Add(orderExamination);
            }

            try
            {
                await uow.SaveAsync();
                return new OrderItemResponse(existingItem);
            }
            catch (Exception ex)
            {
                return new OrderItemResponse($"Во время обновления приказа произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        private async Task<OrderExamination> FindExaminationAsync(int id)
        {
            return await uow.OrderExaminations.FindAsync(id);
        }

        private async Task<OrderItem> FindItemAsync(int id)
        {
            var result = await uow.OrderItems.FindAsync(id);
            return result == null || result.IsDeleted ? null : result;
        }

        private async Task<TargetGroup> FindTargetGroup(int id)
        {
            if (Enum.IsDefined(typeof(TargetGroupId), id))
                return await uow.TargetGroups.FirstOrDefaultAsync(g => g.Id == (TargetGroupId)id);
            return null;
        }

        async Task<OrderItemResponse> IOrderService.FindItemWithActualServicesAsync(int id, int clinicId)
        {
            var item = await uow.OrderItems.FindItemWithActualServicesAsync(id, clinicId);
            if (item == null)
                return new OrderItemResponse("Элемент приказа не найден");
            return new OrderItemResponse(item);
        }

        async Task<IEnumerable<OrderItem>> IOrderService.GetGeneralOrderItemsAsync()
        {
            return await uow.OrderItems.FindAsync(item => item.OrderAnnexId == OrderAnnexId.General && !item.IsDeleted);
        }

        #endregion Methods
    }
}
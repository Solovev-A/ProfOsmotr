using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ProfessionService : IProfessionService
    {
        #region Fields

        private readonly IOrderService orderService;
        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public ProfessionService(IProfUnitOfWork uow, IOrderService orderService)
        {
            this.uow = uow;
            this.orderService = orderService;
        }

        public ProfessionService(IOrderService orderService, IProfUnitOfWork uow)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #endregion Constructors

        #region Methods

        public async Task<ProfessionResponse> CreateProfession(CreateProfessionRequest request)
        {
            var response = await CreateProfessionResponse(request);
            if (response.Succeed)
            {
                try
                {
                    await uow.Professions.AddAsync(response.Result);
                    await uow.SaveAsync();
                }
                catch (Exception ex)
                {
                    return new ProfessionResponse(ex.Message);
                }
            }

            return response;
        }

        public async Task<ProfessionResponse> CreateProfessionForCalculation(CreateProfessionRequest request, int clinicId)
        {
            return await CreateProfessionResponse(request, clinicId);
        }

        public async Task<Profession> FindProfessionAsync(int id)
        {
            return await uow.Professions.FindAsync(id);
        }

        public async Task<ProfessionSearchResultResponse> FindProfessionWithSuggestions(FindProfessionRequest request)
        {
            try
            {
                var itemsResult = await uow.Professions.ExecuteQuery(length: 20, search: request.Search);

                IEnumerable<Profession> suggestions = new Profession[0];
                if (request.EmployerId.HasValue)
                {
                    suggestions = await uow.Professions.GetSuggestedProfessions(request.Search, request.EmployerId.Value);
                }

                var items = itemsResult.Items.Except(suggestions, new ProfessionByIdEqualityComparer());

                var result = new ProfessionSearchResult()
                {
                    Items = items,
                    Suggestions = suggestions
                };

                return new ProfessionSearchResultResponse(result);
            }
            catch (Exception ex)
            {
                return new ProfessionSearchResultResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Profession>> GetAllProfessionsAsync()
        {
            return await uow.Professions.GetAllAsync();
        }

        public async Task<IEnumerable<Profession>> ListFavoritesAsync()
        {
            var professions = await uow.Professions.GetAllAsync();
            return professions
                .GroupBy(value => value, new ProfessionByNameEqualityComparer())
                .Select(group => (group.Key, group.Count()))
                .OrderByDescending(i => i.Item2)
                .Take(150)
                .Select(i => i.Key);
        }

        private async Task<ProfessionResponse> CreateProfessionResponse(CreateProfessionRequest request, int? clinicId = null)
        {
            if (request is null)
                return new ProfessionResponse("Запрос не может быть пустым");

            var profession = new Profession() { Name = request.Name };

            foreach (var id in request.OrderItemIdentifiers)
            {
                var itemResponse = clinicId.HasValue
                    ? await orderService.FindItemWithActualServicesAsync(id, clinicId.Value)
                    : await orderService.FindItemAsync(id);

                if (!itemResponse.Succeed)
                    return new ProfessionResponse(itemResponse.Message);
                profession.OrderItems.Add(itemResponse.Result);
            }

            return new ProfessionResponse(profession);
        }

        #endregion Methods
    }
}
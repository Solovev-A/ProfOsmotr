using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ClinicService : IClinicService
    {
        #region Fields

        private readonly ICatalogService catalogService;
        private readonly IMapper mapper;
        private readonly IProfUnitOfWork uow;
        private readonly IUserService userService;

        #endregion Fields

        #region Constructors

        public ClinicService(IProfUnitOfWork uow, ICatalogService catalogService, IUserService userService, IMapper mapper)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region Methods

        public async Task<ClinicResponse> AddClinic(int requestId)
        {
            var request = await uow.ClinicRegisterRequests.FindAsync(requestId);
            if (request == null)
                return new ClinicResponse($"Запрос с id {requestId} не найден");
            if (request.Approved)
                return new ClinicResponse("Запрос уже был одобрен ранее");

            var existingClinic = await uow.Clinics
                .FirstOrDefaultAsync(c => c.ClinicDetails.FullName == request.FullName || c.ClinicDetails.ShortName == request.ShortName);
            if (existingClinic != null)
                return new ClinicResponse("Клиника уже существует");

            Clinic clinic = CreateClinic(request);
            request.Processed = request.Approved = true;

            try
            {
                await uow.Clinics.AddAsync(clinic);
                await catalogService.CreateDefaultCatalog(clinic);
                AddClinicModerator(clinic, request);
                await uow.SaveAsync();
                return new ClinicResponse(clinic);
            }
            catch (Exception ex)
            {
                return new ClinicResponse($"Во время добавления клиники возникла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<RegisterRequestResponse> AddRegisterRequest(RegisterDataRequest request)
        {
            if (request is null)
                return new RegisterRequestResponse("Запрос не может быть пустым");
            var existingUser = await uow.Users.FirstOrDefaultAsync(u => u.Username == request.User.Username);
            if (existingUser != null)
                return new RegisterRequestResponse("Пользователь с таким логином уже существует");

            var clinicRegisterRequest = mapper.Map<RegisterDataRequest, ClinicRegisterRequest>(request);

            var userResponse = await userService.AddUserAsync(null, request.User);
            if (!userResponse.Succeed)
                return new RegisterRequestResponse(userResponse.Message);
            clinicRegisterRequest.Sender = userResponse.Result;
            clinicRegisterRequest.CreationTime = DateTime.Now;

            try
            {
                await uow.ClinicRegisterRequests.AddAsync(clinicRegisterRequest);
                await uow.SaveAsync();
                return new RegisterRequestResponse(clinicRegisterRequest);
            }
            catch (Exception ex)
            {
                return new RegisterRequestResponse($"При добавлении заявки возникла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<ClinicResponse> GetClinic(int clinicId)
        {
            var clinic = await uow.Clinics.FindAsync(clinicId);
            if (clinic == null)
                return new ClinicResponse("Медицинская организация не найдена");
            if (clinic.IsBlocked)
                return new ClinicResponse("Медицинская организация заблокирована");

            return new ClinicResponse(clinic);
        }

        public async Task<QueryResponse<Clinic>> ListClinics<TKey>(
            int start,
            int length,
            string search,
            Expression<Func<Clinic, TKey>> orderingSelector,
            bool descending)
        {
            try
            {
                var result = await uow.Clinics.ExecuteQuery(orderingSelector, descending, start, length, search, null);
                return new QueryResponse<Clinic>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<Clinic>(ex.Message);
            }
        }

        public async Task<QueryResponse<ClinicRegisterRequest>> ListNewRegisterRequests<TKey>(
            int start,
            int length,
            string search,
            Expression<Func<ClinicRegisterRequest, TKey>> orderingSelector,
            bool descending)
        {
            try
            {
                var result = await uow.ClinicRegisterRequests.ExecuteQuery(
                    orderingSelector,
                    descending,
                    start,
                    length,
                    search,
                    x => !x.Processed);
                return new QueryResponse<ClinicRegisterRequest>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<ClinicRegisterRequest>(ex.Message);
            }
        }

        public async Task<QueryResponse<ClinicRegisterRequest>> ListProcessedRegisterRequests<TKey>(
            int start,
            int length,
            string search,
            Expression<Func<ClinicRegisterRequest, TKey>> orderingSelector,
            bool descending)
        {
            try
            {
                var result = await uow.ClinicRegisterRequests.ExecuteQuery(
                    orderingSelector,
                    descending,
                    start,
                    length,
                    search,
                    x => x.Processed);
                return new QueryResponse<ClinicRegisterRequest>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<ClinicRegisterRequest>(ex.Message);
            }
        }

        public async Task<ClinicResponse> ManageClinic(ManageClinicRequest request)
        {
            if (request is null)
                return new ClinicResponse("Запрос не может быть пустым");
            var clinic = await uow.Clinics.FindAsync(request.Id);
            if (clinic == null)
                return new ClinicResponse("Клиника не найдена");
            if (!(request.NeedBlock ^ clinic.IsBlocked))
                return new ClinicResponse("Данный статус уже задан");

            clinic.IsBlocked = request.NeedBlock;

            try
            {
                await uow.SaveAsync();
                return new ClinicResponse(clinic);
            }
            catch (Exception ex)
            {
                return new ClinicResponse(ex.Message);
            }
        }

        public async Task<RegisterRequestResponse> RejectRegisterRequest(int requestId)
        {
            var request = await uow.ClinicRegisterRequests.FindAsync(requestId);
            if (request == null)
                return new RegisterRequestResponse($"Запрос с id {requestId} не найден");

            if (request.Processed)
                return new RegisterRequestResponse("Запрос уже обработан");

            request.Processed = true;
            request.Approved = false;

            try
            {
                await uow.SaveAsync();
                return new RegisterRequestResponse(request);
            }
            catch (Exception ex)
            {
                return new RegisterRequestResponse($"При обработке заявки возникла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<ClinicResponse> UpdateDetails(UpdateClinicDetailsRequest request)
        {
            if (request is null)
                return new ClinicResponse("Запрос не может быть пустым");
            var clinic = await uow.Clinics.FindAsync(request.ClinicId);
            if (clinic == null)
                return new ClinicResponse("Медицинская организация не найдена");

            mapper.Map(request, clinic.ClinicDetails);

            try
            {
                await uow.SaveAsync();
                return new ClinicResponse(clinic);
            }
            catch (Exception ex)
            {
                return new ClinicResponse($"Во время обновления параметров клиники возникла непредвиденная ошибка: {ex.Message}");
            }
        }

        private void AddClinicModerator(Clinic clinic, ClinicRegisterRequest request)
        {
            request.Sender.Clinic = clinic;
            request.Sender.RoleId = RoleId.ClinicModerator;
        }

        private Clinic CreateClinic(ClinicRegisterRequest request)
        {
            return mapper.Map<ClinicRegisterRequest, Clinic>(request);
        }

        #endregion Methods
    }
}
using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ExaminationsService : IExaminationsService
    {
        private readonly IProfUnitOfWork uow;
        private readonly IPatientService patientService;
        private readonly IUserService userService;
        private readonly IEmployerService employerService;
        private readonly IProfessionService professionService;
        private readonly IOrderService orderService;

        public ExaminationsService(IProfUnitOfWork uow,
                                   IPatientService patientService,
                                   IUserService userService,
                                   IEmployerService employerService,
                                   IProfessionService professionService,
                                   IOrderService orderService)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.employerService = employerService ?? throw new ArgumentNullException(nameof(employerService));
            this.professionService = professionService ?? throw new ArgumentNullException(nameof(professionService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<PreliminaryMedicalExaminationResponse> GetPreliminaryMedicalExaminationAsync(int id)
        {
            var result = await uow.PreliminaryMedicalExaminations.FindExaminationAsync(id);

            if (result is null)
            {
                return new PreliminaryMedicalExaminationResponse("Медосмотр не найден");
            }
            return new PreliminaryMedicalExaminationResponse(result);
        }

        public async Task<int> GetPreliminaryMedicalExaminationClinicIdAsync(int examinationId)
        {
            var examination = await uow.PreliminaryMedicalExaminations.FindAsync(examinationId);
            return examination?.ClinicId ?? -1;
        }

        public async Task<QueryResponse<PreliminaryMedicalExamination>> ListActualPreliminaryMedicalExaminationsAsync(int clinicId)
        {
            try
            {
                var result = await uow.PreliminaryMedicalExaminations.ExecuteQuery(
                    orderingSelector: ex => ex.Id,
                    descending: true,
                    length: 20,
                    customFilter: ex => ex.ClinicId == clinicId);

                return new QueryResponse<PreliminaryMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PreliminaryMedicalExamination>(ex.Message);
            }
        }

        public async Task<QueryResponse<PreliminaryMedicalExamination>> ListPreliminaryMedicalExaminationsAsync(
            ExecutePreliminaryExaminationsQueryRequest request)
        {
            Expression<Func<PreliminaryMedicalExamination, bool>> customFilter = ex => ex.ClinicId == request.ClinicId;
            if (request.EmployerId.HasValue)
            {
                customFilter = customFilter.AndAlso(ex => ex.EmployerId == request.EmployerId.Value);
            }

            try
            {
                var result = await uow.PreliminaryMedicalExaminations.ExecuteQuery(
                    request.Start,
                    request.ItemsPerPage,
                    request.Search,
                    customFilter);

                return new QueryResponse<PreliminaryMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PreliminaryMedicalExamination>(ex.Message);
            }
        }

        public async Task<PreliminaryMedicalExaminationResponse> CreatePreliminaryMedicalExaminationAsync(
            CreatePreliminaryMedicalExaminationRequest request)
        {
            var creatorResponse = await userService.GetUser(request.CreatorId);
            if (!creatorResponse.Succeed)
            {
                return new PreliminaryMedicalExaminationResponse(creatorResponse.Message);
            }

            var patientResponse = await patientService.CheckPatientAsync(request.PatientId);
            if (!patientResponse.Succeed)
            {
                return new PreliminaryMedicalExaminationResponse(patientResponse.Message);
            }

            var examination = new PreliminaryMedicalExamination()
            {
                ClinicId = patientResponse.Result.ClinicId,
                LastEditorId = request.CreatorId,
                CheckupStatus = new IndividualCheckupStatus()
                {
                    LastEditorId = request.CreatorId,
                    PatientId = patientResponse.Result.Id
                }
            };

            try
            {
                await uow.PreliminaryMedicalExaminations.AddAsync(examination);
                await uow.SaveAsync();
                return new PreliminaryMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PreliminaryMedicalExaminationResponse(ex.Message);
            }
        }

        public async Task<PreliminaryMedicalExaminationResponse> UpdatePreliminaryExaminationAsync(UpdatePreliminaryExaminationRequest request)
        {
            var examination = await uow.PreliminaryMedicalExaminations.FindExaminationAsync(request.ExaminationId, false);
            if (examination is null)
            {
                return new PreliminaryMedicalExaminationResponse("Медосмотр не найен");
            }

            var updateLastEditorResult = await UpdateLastEditor(examination, request.EditorId);
            if (!updateLastEditorResult.Succeed)
            {
                return new PreliminaryMedicalExaminationResponse(updateLastEditorResult.ErrorMessage);
            }

            var query = request.Query;

            if (query.IsFieldPresent(nameof(query.EmployerId)))
            {
                var result = await UpdateEmployer(examination, query.EmployerId);
                if (!result.Succeed)
                {
                    return new PreliminaryMedicalExaminationResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.EmployerDepartmentId)))
            {
                var result = await UpdateEmployerDepartment(examination, examination.CheckupStatus, query.EmployerDepartmentId);
                if (!result.Succeed)
                {
                    return new PreliminaryMedicalExaminationResponse(result.ErrorMessage);
                }

            }
            if (query.IsFieldPresent(nameof(query.ProfessionId)))
            {
                var result = await UpdateProfession(examination.CheckupStatus, query.ProfessionId);
                if (!result.Succeed)
                {
                    return new PreliminaryMedicalExaminationResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.CheckupIndexValues)))
            {
                await UpdateCheckupIndexValues(examination.CheckupStatus.IndividualCheckupIndexValues, query.CheckupIndexValues);
            }
            if (query.IsFieldPresent(nameof(query.CheckupResultId)))
            {
                UpdateCheckupResult(examination, examination.CheckupStatus, query.CheckupResultId);
            }
            if (query.IsFieldPresent(nameof(query.DateOfComplition)))
            {
                examination.CheckupStatus.DateOfCompletion = query.DateOfComplition;
            }
            if (query.IsFieldPresent(nameof(query.MedicalReport)))
            {
                examination.CheckupStatus.MedicalReport = query.MedicalReport;
            }
            if (query.IsFieldPresent(nameof(query.RegistrationJournalEntryNumber)))
            {
                examination.CheckupStatus.RegistrationJournalEntryNumber = query.RegistrationJournalEntryNumber;
            }

            try
            {
                await uow.SaveAsync();
                return new PreliminaryMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PreliminaryMedicalExaminationResponse(ex.Message);
            }
        }

        public async Task<PreliminaryMedicalExaminationResponse> DeletePreliminaryExaminationAsync(int examinationId)
        {
            var examination = await uow.PreliminaryMedicalExaminations.FindAsync(examinationId);

            if (examination is null)
            {
                return new PreliminaryMedicalExaminationResponse("Медосмотр не найен");
            }

            try
            {
                uow.PreliminaryMedicalExaminations.Delete(examinationId);
                await uow.SaveAsync();
                return new PreliminaryMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PreliminaryMedicalExaminationResponse(ex.Message);
            }
        }

        public async Task<PeriodicMedicalExaminationResponse> GetPeriodicMedicalExaminationAsync(int id)
        {
            var result = await uow.PeriodicMedicalExaminations.FindExaminationAsync(id);

            if (result is null)
            {
                return new PeriodicMedicalExaminationResponse("Медосмотр не найден");
            }
            return new PeriodicMedicalExaminationResponse(result);
        }

        public async Task<int> GetPeriodicMedicalExaminationClinicIdAsync(int examinationId)
        {
            var examination = await uow.PeriodicMedicalExaminations.FindAsync(examinationId);
            return examination?.ClinicId ?? -1;
        }

        public async Task<QueryResponse<PeriodicMedicalExamination>> ListPeriodicMedicalExaminationsAsync(ExecuteQueryBaseRequest request)
        {
            try
            {
                var result = await uow.PeriodicMedicalExaminations.ExecuteQuery(
                    request.Start,
                    request.ItemsPerPage,
                    request.Search,
                    ex => ex.ClinicId == request.ClinicId);

                return new QueryResponse<PeriodicMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PeriodicMedicalExamination>(ex.Message);
            }
        }

        public async Task<QueryResponse<PeriodicMedicalExamination>> ListActualPeriodicMedicalExaminationsAsync(int clinicId)
        {
            try
            {
                var result = await uow.PeriodicMedicalExaminations.ExecuteQuery(
                    orderingSelector: ex => ex.Id,
                    descending: true,
                    length: 20,
                    customFilter: ex => ex.ClinicId == clinicId);

                return new QueryResponse<PeriodicMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PeriodicMedicalExamination>(ex.Message);
            }
        }

        public async Task<PeriodicMedicalExaminationResponse> DeletePeriodicExaminationAsync(int id)
        {
            var examination = await uow.PeriodicMedicalExaminations.FindAsync(id);

            if (examination is null)
            {
                return new PeriodicMedicalExaminationResponse("Медосмотр не найен");
            }

            try
            {
                uow.PeriodicMedicalExaminations.Delete(id);
                await uow.SaveAsync();
                return new PeriodicMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PeriodicMedicalExaminationResponse(ex.Message);
            }
        }

        public async Task<ContingentCheckupStatusResponse> CreateContingentCheckupStatus(CreateContingentCheckupStatusRequest request)
        {
            var examination = await uow.PeriodicMedicalExaminations.FindAsync(request.ExaminationId);
            if (examination is null)
            {
                return new ContingentCheckupStatusResponse("Медосмотр не найден");
            }

            var patientResponse = await patientService.CheckPatientAsync(request.PatientId);
            if (!patientResponse.Succeed)
            {
                return new ContingentCheckupStatusResponse(patientResponse.Message);
            }

            var creatorResponse = await userService.GetUser(request.CreatorId);
            if (!creatorResponse.Succeed)
            {
                return new ContingentCheckupStatusResponse(creatorResponse.Message);
            }

            var checkupStatus = new ContingentCheckupStatus()
            {
                PeriodicMedicalExaminationId = request.ExaminationId,
                PatientId = request.PatientId,
                LastEditorId = request.CreatorId
            };

            var updateDepartmentResult = await UpdateEmployerDepartment(examination, checkupStatus, request.EmployerDepartmentId);
            if (!updateDepartmentResult.Succeed)
            {
                return new ContingentCheckupStatusResponse(updateDepartmentResult.ErrorMessage);
            }

            var updateProfessionResult = await UpdateProfession(checkupStatus, request.ProfessionId);
            if (!updateProfessionResult.Succeed)
            {
                return new ContingentCheckupStatusResponse(updateProfessionResult.ErrorMessage);
            }

            try
            {
                examination.Statuses.Add(checkupStatus);
                await uow.SaveAsync();
                return new ContingentCheckupStatusResponse(checkupStatus);
            }
            catch (Exception ex)
            {
                return new ContingentCheckupStatusResponse(ex.Message);
            }
        }

        protected async Task<ServiceActionResult> UpdateLastEditor(MedicalExamination examination, int editorId)
        {
            var editorResponse = await userService.GetUser(editorId);
            if (!editorResponse.Succeed)
            {
                return new ServiceActionResult(editorResponse.Message);
            }
            examination.LastEditor = editorResponse.Result;
            return new ServiceActionResult();
        }

        protected async Task<ServiceActionResult> UpdateEmployer(MedicalExamination examination, int? newEmployerId)
        {
            if (newEmployerId.HasValue)
            {
                var employerResponse = await employerService.GetEmployerAsync(newEmployerId.Value);
                if (!employerResponse.Succeed)
                {
                    return new ServiceActionResult(employerResponse.Message);
                }
                examination.Employer = employerResponse.Result;
            }
            else
            {
                examination.Employer = null;
            }
            return new ServiceActionResult();
        }

        protected async Task<ServiceActionResult> UpdateEmployerDepartment(MedicalExamination examination,
                                                                            CheckupStatus checkupStatus,
                                                                            int? newEmployerDepartmentId)
        {
            if (newEmployerDepartmentId.HasValue)
            {
                if (examination.Employer is null)
                {
                    return new ServiceActionResult("Невозможно задать структурное подразделение, если не задана организация");
                }
                var departmentResponse = await employerService.FindEmployerDepartmentAsync(newEmployerDepartmentId.Value);
                if (!departmentResponse.Succeed)
                {
                    return new ServiceActionResult(departmentResponse.Message);
                }
                if (examination.Employer.Id != departmentResponse.Result.ParentId)
                {
                    return new ServiceActionResult("Структурное подразделение не принадлежит организации");
                }
            }
            checkupStatus.EmployerDepartmentId = newEmployerDepartmentId;
            return new ServiceActionResult();
        }

        protected async Task<ServiceActionResult> UpdateProfession(CheckupStatus checkupStatus, int? newProfessionId)
        {
            if (newProfessionId.HasValue)
            {
                var profession = await professionService.FindProfessionAsync(newProfessionId.Value);
                if (profession is null)
                {
                    return new ServiceActionResult("Профессия не найдена");
                }
            }
            checkupStatus.ProfessionId = newProfessionId;
            return new ServiceActionResult();
        }

        protected void UpdateCheckupResult(MedicalExamination examination,
                                           CheckupStatus checkupStatus,
                                           CheckupResultId? newCheckupResultId)
        {
            checkupStatus.CheckupResultId = newCheckupResultId;
            if (newCheckupResultId.HasValue && newCheckupResultId.Value != CheckupResultId.NeedAdditionalMedicalExamination)
            {
                examination.Completed = true;
            }
            else
            {
                examination.Completed = false;
            }
        }

        protected async Task<ServiceActionResult> UpdateCheckupIndexValues<T>(ICollection<T> data, IEnumerable<UpdateCheckupIndexValueRequest> requests)
            where T : CheckupIndexValue, new()
        {
            foreach (var request in requests)
            {
                T checkupIndexValue = data.FirstOrDefault(i => request.Id == i.ExaminationResultIndexId);

                if (checkupIndexValue != null)
                {
                    if (request.Value == string.Empty)
                    {
                        data.Remove(checkupIndexValue);
                    }
                    else
                    {
                        checkupIndexValue.Value = request.Value;
                    }
                }
                else
                {
                    var index = await orderService.FindExaminationResultIndexAsync(request.Id);
                    if (index is null)
                    {
                        return new ServiceActionResult($"Показатель id {request.Id} не найден");
                    }
                    T newCheckupIndexValue = new T()
                    {
                        ExaminationResultIndex = index,
                        Value = request.Value
                    };
                    data.Add(newCheckupIndexValue);
                }
            }
            return new ServiceActionResult();
        }
    }
}
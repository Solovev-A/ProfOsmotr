using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ExaminationsService : IExaminationsService
    {
        private readonly IProfUnitOfWork uow;
        private readonly IPatientService patientService;
        private readonly IUserService userService;

        public ExaminationsService(IProfUnitOfWork uow, IPatientService patientService, IUserService userService)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        public async Task<PreliminaryMedicalExaminationResponse> DeleteExaminationAsync(int examinationId)
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
    }
}
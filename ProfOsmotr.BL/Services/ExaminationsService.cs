using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IICD10Service icd10Service;
        private readonly IMapper mapper;
        private readonly IReportDataFactory reportDataFactory;
        private readonly IReportsCreator reportsCreator;

        public ExaminationsService(IProfUnitOfWork uow,
                                   IPatientService patientService,
                                   IUserService userService,
                                   IEmployerService employerService,
                                   IProfessionService professionService,
                                   IOrderService orderService,
                                   IICD10Service icd10Service,
                                   IMapper mapper,
                                   IReportDataFactory reportDataFactory,
                                   IReportsCreator reportsCreator)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.employerService = employerService ?? throw new ArgumentNullException(nameof(employerService));
            this.professionService = professionService ?? throw new ArgumentNullException(nameof(professionService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.icd10Service = icd10Service ?? throw new ArgumentNullException(nameof(icd10Service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.reportDataFactory = reportDataFactory ?? throw new ArgumentNullException(nameof(reportDataFactory));
            this.reportsCreator = reportsCreator ?? throw new ArgumentNullException(nameof(reportsCreator));
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
                var result = await UpdateCheckupIndexValues(examination.CheckupStatus.IndividualCheckupIndexValues, query.CheckupIndexValues);
                if (!result.Succeed)
                {
                    return new PreliminaryMedicalExaminationResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.CheckupResultId)))
            {
                UpdateCheckupResult(examination, examination.CheckupStatus, query.CheckupResultId);
            }
            if (query.IsFieldPresent(nameof(query.DateOfCompletion)))
            {
                examination.CheckupStatus.DateOfCompletion = query.DateOfCompletion;
            }
            if (!examination.CheckupStatus.CheckupResultId.HasValue && examination.CheckupStatus.DateOfCompletion.HasValue)
            {
                return new PreliminaryMedicalExaminationResponse("Необходимо указать результат для завершенного медосмотра");
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

        public async Task<QueryResponse<PreliminaryMedicalExamination>> GetPreliminaryMedicalExaminationsJournalAsync(
            ExecuteExaminationsJournalQueryRequest request)
        {
            try
            {
                var result = await uow.PreliminaryMedicalExaminations.ExecuteQuery(
                                start: request.Start,
                                length: request.ItemsPerPage,
                                customFilter: (ex) => ex.ClinicId == request.ClinicId
                                        && ex.CheckupStatus.DateOfCompletion.HasValue
                                        && ex.CheckupStatus.RegistrationJournalEntryNumber.HasValue
                                        && ex.CheckupStatus.DateOfCompletion.Value.Year == request.Year,
                                orderingSelector: (ex) => ex.CheckupStatus.RegistrationJournalEntryNumber.Value,
                                descending: false);

                return new QueryResponse<PreliminaryMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PreliminaryMedicalExamination>(ex.Message);
            }
        }

        public async Task<PeriodicMedicalExaminationResponse> GetPeriodicMedicalExaminationAsync(int id)
        {
            var result = await uow.PeriodicMedicalExaminations.FindExaminationAsync(id, true);

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

        public async Task<QueryResponse<PeriodicMedicalExamination>> GetPeriodicMedicalExaminationsJournalAsync(
            ExecuteExaminationsJournalQueryRequest request)
        {
            try
            {
                var result = await uow.PeriodicMedicalExaminations.ExecuteQuery(
                                start: request.Start,
                                length: request.ItemsPerPage,
                                customFilter: (ex) => ex.ClinicId == request.ClinicId
                                        && ex.ExaminationYear == request.Year,
                                orderingSelector: (ex) => ex.ReportDate,
                                descending: false);

                return new QueryResponse<PeriodicMedicalExamination>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<PeriodicMedicalExamination>(ex.Message);
            }
        }

        public async Task<PeriodicMedicalExaminationResponse> CreatePeriodicMedicalExaminationAsync(
            CreatePeriodicMedicalExaminationRequest request)
        {
            var creatorResponse = await userService.GetUser(request.CreatorId);
            if (!creatorResponse.Succeed)
            {
                return new PeriodicMedicalExaminationResponse(creatorResponse.Message);
            }

            var employerResponse = await employerService.GetEmployerAsync(request.EmployerId);
            if (!employerResponse.Succeed)
            {
                return new PeriodicMedicalExaminationResponse(employerResponse.Message);
            }

            Employer employer = employerResponse.Result;

            var examination = new PeriodicMedicalExamination()
            {
                ClinicId = employer.ClinicId,
                EmployerId = request.EmployerId,
                ExaminationYear = request.ExaminationYear,
                LastEditorId = request.CreatorId
            };

            try
            {
                await uow.PeriodicMedicalExaminations.AddAsync(examination);
                await uow.SaveAsync();
                return new PeriodicMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PeriodicMedicalExaminationResponse(ex.Message);
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

        public async Task<PeriodicMedicalExaminationResponse> UpdatePeriodicExaminationAsync(UpdatePeriodicExaminationRequest request)
        {
            var examination = await uow.PeriodicMedicalExaminations.FindExaminationAsync(request.ExaminationId);
            if (examination is null)
            {
                return new PeriodicMedicalExaminationResponse("Медосмотр не найден");
            }

            if (request.Query.IsFieldPresent(nameof(request.Query.ContingentGroupMedicalReport)))
            {
                var query = request.Query.ContingentGroupMedicalReport;
                foreach (var checkupStatusId in query.CheckupStatuses)
                {
                    var checkupStatus = examination.Statuses.FirstOrDefault(status => status.Id == checkupStatusId);

                    if (checkupStatus is null)
                    {
                        return new PeriodicMedicalExaminationResponse($"Медосмотр работника с id {checkupStatusId} не принадлежит периодическому медосмотру");
                    }

                    mapper.Map(query, checkupStatus);

                    if (query.DateOfCompletion.HasValue)
                    {
                        checkupStatus.CheckupStarted = true;
                    }

                    if (!checkupStatus.CheckupStarted && checkupStatus.DateOfCompletion.HasValue)
                    {
                        return new PeriodicMedicalExaminationResponse("Медосмотр с датой завершения не может быть не начатым");
                    }
                    if (!checkupStatus.CheckupStarted && checkupStatus.CheckupResultId.HasValue)
                    {
                        return new PeriodicMedicalExaminationResponse("Медосмотр с результатом не может быть не начатым");
                    }
                    if (!checkupStatus.CheckupResultId.HasValue && checkupStatus.DateOfCompletion.HasValue)
                    {
                        return new PeriodicMedicalExaminationResponse("Необходимо указать результат для завершенного медосмотра");
                    }
                }
            }

            mapper.Map(request.Query, examination);

            if (request.Query.IsFieldPresent(nameof(request.Query.ReportDate)))
            {
                examination.Completed = request.Query.ReportDate.HasValue;
            }

            try
            {
                await uow.SaveAsync();
                return new PeriodicMedicalExaminationResponse(examination);
            }
            catch (Exception ex)
            {
                return new PeriodicMedicalExaminationResponse(ex.Message);
            }
        }

        public async Task<ContingentCheckupStatusResponse> GetContingentCheckupStatus(int id)
        {
            var checkupStatus = await uow.PeriodicMedicalExaminations.FindCheckupStatus(id, true);

            if (checkupStatus is null)
            {
                return new ContingentCheckupStatusResponse("Запись медосмотра не найдена");
            }

            return new ContingentCheckupStatusResponse(checkupStatus);
        }

        public async Task<int> GetContingentCheckupStatusClinicIdAsync(int checkupStatusId)
        {
            var clinicId = await uow.PeriodicMedicalExaminations.GetCheckupStatusClinicIdAsync(checkupStatusId);

            return clinicId;
        }

        public async Task<ContingentCheckupStatusResponse> UpdateContingentCheckupStatusAsync(UpdateContingentCheckupStatusRequest request)
        {
            var checkupStatus = await uow.PeriodicMedicalExaminations.FindCheckupStatus(request.CheckupStatusId);
            if (checkupStatus is null)
            {
                return new ContingentCheckupStatusResponse("Запись медосмотра не найдена");
            }

            var editorResponse = await userService.GetUser(request.EditorId);
            if (!editorResponse.Succeed)
            {
                return new ContingentCheckupStatusResponse(editorResponse.Message);
            }
            checkupStatus.LastEditorId = request.EditorId;

            var query = request.Query;

            if (query.IsFieldPresent(nameof(query.EmployerDepartmentId)))
            {
                var result = await UpdateEmployerDepartment(checkupStatus.PeriodicMedicalExamination, checkupStatus, query.EmployerDepartmentId);
                if (!result.Succeed)
                {
                    return new ContingentCheckupStatusResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.ProfessionId)))
            {
                var result = await UpdateProfession(checkupStatus, query.ProfessionId);
                if (!result.Succeed)
                {
                    return new ContingentCheckupStatusResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.CheckupIndexValues)))
            {
                var result = await UpdateCheckupIndexValues(checkupStatus.ContingentCheckupIndexValues, query.CheckupIndexValues);
                if (!result.Succeed)
                {
                    return new ContingentCheckupStatusResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.CheckupStarted)))
            {
                checkupStatus.CheckupStarted = query.CheckupStarted;
            }
            if (query.IsFieldPresent(nameof(query.CheckupResultId)))
            {
                checkupStatus.CheckupResultId = query.CheckupResultId;
            }
            if (query.IsFieldPresent(nameof(query.DateOfCompletion)))
            {
                checkupStatus.DateOfCompletion = query.DateOfCompletion;
                if (query.DateOfCompletion.HasValue)
                {
                    checkupStatus.CheckupStarted = true;
                }
            }
            if (!checkupStatus.CheckupStarted && checkupStatus.DateOfCompletion.HasValue)
            {
                return new ContingentCheckupStatusResponse("Медосмотр с датой завершения не может быть не начатым");
            }
            if (!checkupStatus.CheckupStarted && checkupStatus.CheckupResultId.HasValue)
            {
                return new ContingentCheckupStatusResponse("Медосмотр с результатом не может быть не начатым");
            }
            if (!checkupStatus.CheckupResultId.HasValue && checkupStatus.DateOfCompletion.HasValue)
            {
                return new ContingentCheckupStatusResponse("Необходимо указать результат для завершенного медосмотра");
            }
            if (query.IsFieldPresent(nameof(query.MedicalReport)))
            {
                checkupStatus.MedicalReport = query.MedicalReport;
            }
            if (query.IsFieldPresent(nameof(query.RegistrationJournalEntryNumber)))
            {
                checkupStatus.RegistrationJournalEntryNumber = query.RegistrationJournalEntryNumber;
            }
            if (query.IsFieldPresent(nameof(query.IsDisabled)))
            {
                checkupStatus.IsDisabled = query.IsDisabled;
            }
            if (query.IsFieldPresent(nameof(query.NeedExaminationAtOccupationalPathologyCenter)))
            {
                checkupStatus.NeedExaminationAtOccupationalPathologyCenter = query.NeedExaminationAtOccupationalPathologyCenter;
            }
            if (query.IsFieldPresent(nameof(query.NeedOutpatientExamunationAndTreatment)))
            {
                checkupStatus.NeedOutpatientExamunationAndTreatment = query.NeedOutpatientExamunationAndTreatment;
            }
            if (query.IsFieldPresent(nameof(query.NeedInpatientExamunationAndTreatment)))
            {
                checkupStatus.NeedInpatientExamunationAndTreatment = query.NeedInpatientExamunationAndTreatment;
            }
            if (query.IsFieldPresent(nameof(query.NeedSpaTreatment)))
            {
                checkupStatus.NeedSpaTreatment = query.NeedSpaTreatment;
            }
            if (query.IsFieldPresent(nameof(query.NeedDispensaryObservation)))
            {
                checkupStatus.NeedDispensaryObservation = query.NeedDispensaryObservation;
            }
            if (query.IsFieldPresent(nameof(query.NewlyDiagnosedChronicSomaticDiseases)))
            {
                var result = await UpdateNewlyDiagnosedDiseases(checkupStatus.NewlyDiagnosedChronicSomaticDiseases,
                                                                query.NewlyDiagnosedChronicSomaticDiseases);
                if (!result.Succeed)
                {
                    return new ContingentCheckupStatusResponse(result.ErrorMessage);
                }
            }
            if (query.IsFieldPresent(nameof(query.NewlyDiagnosedOccupationalDiseases)))
            {
                var result = await UpdateNewlyDiagnosedDiseases(checkupStatus.NewlyDiagnosedOccupationalDiseases,
                                                                query.NewlyDiagnosedOccupationalDiseases);
                if (!result.Succeed)
                {
                    return new ContingentCheckupStatusResponse(result.ErrorMessage);
                }
            }

            try
            {
                await uow.SaveAsync();
                return new ContingentCheckupStatusResponse(checkupStatus);
            }
            catch (Exception ex)
            {
                return new ContingentCheckupStatusResponse(ex.Message);
            }
        }

        public async Task<ContingentCheckupStatusResponse> DeleteContingentCheckupStatusAsync(int id)
        {
            var checkupStatus = await uow.PeriodicMedicalExaminations.FindCheckupStatus(id);
            if (checkupStatus is null)
            {
                return new ContingentCheckupStatusResponse("Запись медосмотра не найдена");
            }

            try
            {
                uow.PeriodicMedicalExaminations.DeleteCheckupStatus(checkupStatus);
                await uow.SaveAsync();
                return new ContingentCheckupStatusResponse(checkupStatus);
            }
            catch (Exception ex)
            {
                return new ContingentCheckupStatusResponse(ex.Message);
            }
        }

        public async Task<ExaminationsStatisticsResponse> CalculateStatistics(CalculateStatisticsRequest request)
        {
            try
            {
                var preliminaryExaminatinsCount = await uow.PreliminaryMedicalExaminations.CountExaminationsByMonth(request.ClinicId);
                var contingentCheckupStatusesCount = await uow.PeriodicMedicalExaminations.CountCheckupsByMonth(request.ClinicId);

                var firstPreliminaryStatsPeriod = preliminaryExaminatinsCount.FirstOrDefault()?.Period;
                var firstCheckupStatusesStatsPeriod = contingentCheckupStatusesCount.FirstOrDefault()?.Period;

                // опеределяем самый ранний период из представленных в статистике
                var firstPeriod = firstPreliminaryStatsPeriod is null
                    ? firstCheckupStatusesStatsPeriod is null ? null : firstCheckupStatusesStatsPeriod
                    : firstCheckupStatusesStatsPeriod is null
                        ? firstPreliminaryStatsPeriod : string.Compare(firstPreliminaryStatsPeriod, firstCheckupStatusesStatsPeriod) >= 0
                            ? firstCheckupStatusesStatsPeriod : firstPreliminaryStatsPeriod;

                var range = StatisticsHelper.GetPeriodsRange(firstPeriod);

                var result = range
                    .Select(period => new ExaminationsStatisticsData()
                    {
                        Period = period,
                        ContingentCheckupStatusesCount = contingentCheckupStatusesCount.FirstOrDefault(r => r.Period == period)?.Count ?? 0,
                        PreliminaryExaminationsCount = preliminaryExaminatinsCount.FirstOrDefault(r => r.Period == period)?.Count ?? 0
                    });

                return new ExaminationsStatisticsResponse(result);
            }
            catch (Exception ex)
            {
                return new ExaminationsStatisticsResponse(ex.Message);
            }
        }

        public async Task<FileResultResponse> GetPreliminaryExaminationMedicalReportAsync(int examinationId)
        {
            var examination = await uow.PreliminaryMedicalExaminations.FindExaminationAsync(examinationId);

            if (examination is null)
            {
                return new FileResultResponse("Медосмотр не найден");
            }

            return await CreateCheckupStatusMedicalReport(examination.CheckupStatus);
        }

        public async Task<FileResultResponse> GetContingentCheckupStatusMedicalReportAsync(int checkupStatusId)
        {
            var checkupStatus = await uow.PeriodicMedicalExaminations.FindCheckupStatus(checkupStatusId);

            if (checkupStatus is null)
            {
                return new FileResultResponse("Медосмотр работника не найден");
            }

            return await CreateCheckupStatusMedicalReport(checkupStatus);
        }

        #region Protected methods

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
                if (!examination.EmployerId.HasValue)
                {
                    return new ServiceActionResult("Невозможно задать структурное подразделение, если не задана организация");
                }
                var departmentResponse = await employerService.FindEmployerDepartmentAsync(newEmployerDepartmentId.Value);
                if (!departmentResponse.Succeed)
                {
                    return new ServiceActionResult(departmentResponse.Message);
                }
                if (examination.EmployerId.Value != departmentResponse.Result.ParentId)
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

        protected async Task<ServiceActionResult> UpdateCheckupIndexValues<T>(ICollection<T> data,
                                                                              IEnumerable<UpdateCheckupIndexValueRequest> requests)
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

        protected async Task<ServiceActionResult> UpdateNewlyDiagnosedDiseases<T>(ICollection<T> data,
                                                                                  IEnumerable<UpdateNewlyDiagnosedDiseaseRequest> requests)
        where T : NewlyDiagnosedDisease, new()
        {
            foreach (var request in requests)
            {
                T disease = data.FirstOrDefault(d => request.ChapterId == d.ICD10ChapterId);
                if (disease != null)
                {
                    if (request.Cases == 0)
                    {
                        data.Remove(disease);
                    }
                    else
                    {
                        disease.Cases = request.Cases;
                    }
                }
                else
                {
                    if (request.Cases == 0) continue;

                    ICD10Chapter chapter = await icd10Service.FindChapterAsync(request.ChapterId);
                    if (chapter is null)
                    {
                        return new ServiceActionResult($"Класс заболеваний с id {request.ChapterId} не найден");
                    }
                    T newDisease = new T()
                    {
                        Cases = request.Cases,
                        ICD10ChapterId = request.ChapterId
                    };
                    data.Add(newDisease);
                }
            }
            return new ServiceActionResult();
        }

        protected async Task<FileResultResponse> CreateCheckupStatusMedicalReport(CheckupStatus checkupStatus)
        {
            try
            {
                var data = reportDataFactory.CreateCheckupStatusMedicalReportData(checkupStatus);
                var result = await reportsCreator.CreateCheckupStatusMedicalReport(data);
                return new FileResultResponse(result);
            }
            catch (Exception ex)
            {
                return new FileResultResponse(ex.Message);
            }
        }

        #endregion Protected methods
    }
}
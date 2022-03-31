using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для управления медицинскими осмотрами
    /// </summary>
    public interface IExaminationsService
    {
        /// <summary>
        /// Добавляет предварительный медицинский осмотр
        /// </summary>
        /// <param name="request">Запрос для добавления предварительного медосмотра</param>
        /// <returns></returns>
        Task<PreliminaryMedicalExaminationResponse> CreatePreliminaryMedicalExaminationAsync(CreatePreliminaryMedicalExaminationRequest request);

        /// <summary>
        /// Удаляет предварительный медосмотр с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор предварительного медосмотра для удаления</param>
        /// <returns></returns>
        Task<PreliminaryMedicalExaminationResponse> DeletePreliminaryExaminationAsync(int examinationId);

        /// <summary>
        /// Предоставляет предварительный медосмотр с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор предварительного медосмотра</param>
        /// <returns></returns>
        Task<PreliminaryMedicalExaminationResponse> GetPreliminaryMedicalExaminationAsync(int id);

        /// <summary>
        /// Предосатвляет идентификатор медицинской организации, проводившей предварительный осмотр с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор предварительного медосмотра</param>
        /// <returns></returns>
        Task<int> GetPreliminaryMedicalExaminationClinicIdAsync(int examinationId);

        /// <summary>
        /// Предоставляет список актуальных для клиники с идентификатором <paramref name="clinicId"/> предварительных медосмотров
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<QueryResponse<PreliminaryMedicalExamination>> ListActualPreliminaryMedicalExaminationsAsync(int clinicId);

        /// <summary>
        /// Производит поиск предварительных медосмотров, в соответствии с запросом <paramref name="request"/>
        /// </summary>
        /// <param name="request">Запрос для поиска предварительных медосмотров</param>
        /// <returns></returns>
        Task<QueryResponse<PreliminaryMedicalExamination>> ListPreliminaryMedicalExaminationsAsync(ExecutePreliminaryExaminationsQueryRequest request);

        /// <summary>
        /// Обновляет предварительный медосмотр
        /// </summary>
        /// <param name="request">Запрос для обновления предварительного медосмотра</param>
        /// <returns></returns>
        Task<PreliminaryMedicalExaminationResponse> UpdatePreliminaryExaminationAsync(UpdatePreliminaryExaminationRequest request);

        /// <summary>
        /// Производит поиск периодических медосмотров, в соответствии с запросом <paramref name="request"/>
        /// </summary>
        /// <param name="request">Запрос для поиска периодических медосмотров</param>
        /// <returns></returns>
        Task<QueryResponse<PeriodicMedicalExamination>> ListPeriodicMedicalExaminationsAsync(ExecuteQueryBaseRequest request);

        /// <summary>
        /// Предоставляет списк актуальных периодических медосмотров для клиники с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<QueryResponse<PeriodicMedicalExamination>> ListActualPeriodicMedicalExaminationsAsync(int clinicId);

        /// <summary>
        /// Предоставляет периодический медицинский осмотр с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра</param>
        /// <returns></returns>
        Task<PeriodicMedicalExaminationResponse> GetPeriodicMedicalExaminationAsync(int id);

        /// <summary>
        /// Предоставляет идентификатор медицинской организации, проводившей периодический медосмотр 
        /// с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор периодического медосмотра</param>
        /// <returns></returns>
        Task<int> GetPeriodicMedicalExaminationClinicIdAsync(int examinationId);

        /// <summary>
        /// Удаляет периодический медосмотр с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра для удаления</param>
        /// <returns></returns>
        Task<PeriodicMedicalExaminationResponse> DeletePeriodicExaminationAsync(int id);

        /// <summary>
        /// Добавляет периодический медосмотр работника
        /// </summary>
        /// <param name="request">Запрос для добавления периодического медосмотра работника</param>
        /// <returns></returns>
        Task<ContingentCheckupStatusResponse> CreateContingentCheckupStatus(CreateContingentCheckupStatusRequest request);

        /// <summary>
        /// Обновляет периодический медосмотр
        /// </summary>
        /// <param name="request">Запрос для обновления периодического медосмотра</param>
        /// <returns></returns>
        Task<PeriodicMedicalExaminationResponse> UpdatePeriodicExaminationAsync(UpdatePeriodicExaminationRequest request);

        /// <summary>
        /// Предоставляет периодический медосмотр работника с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ContingentCheckupStatusResponse> GetContingentCheckupStatus(int id);

        /// <summary>
        /// Предоставляет идентификатор медицинской организации, проводившей периодический медосмотр работника с идентификатором <paramref name="checkupStatusId"/>
        /// </summary>
        /// <param name="checkupStatusId">Идентификатор периодического медосмотра работника</param>
        /// <returns></returns>
        Task<int> GetContingentCheckupStatusClinicIdAsync(int checkupStatusId);

        /// <summary>
        /// Обновляет периодический осмотр работника
        /// </summary>
        /// <param name="request">Запрос для обновления периодического медосмотра работника</param>
        /// <returns></returns>
        Task<ContingentCheckupStatusResponse> UpdateContingentCheckupStatusAsync(UpdateContingentCheckupStatusRequest request);

        /// <summary>
        /// Удаляет периодический медосмотр работника с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра работника</param>
        /// <returns></returns>
        Task<ContingentCheckupStatusResponse> DeleteContingentCheckupStatusAsync(int id);

        /// <summary>
        /// Предоставляет журнал предварительных медицинских осмотров
        /// </summary>
        /// <param name="request">Запрос для журнала предварительных медосмотров</param>
        /// <returns></returns>
        Task<QueryResponse<PreliminaryMedicalExamination>> GetPreliminaryMedicalExaminationsJournalAsync(ExecuteExaminationsJournalQueryRequest request);

        /// <summary>
        /// Предоставляет журнал периодических медицинских осмотров
        /// </summary>
        /// <param name="request">Запрос для журнала периодических медосмотров</param>
        /// <returns></returns>
        Task<QueryResponse<PeriodicMedicalExamination>> GetPeriodicMedicalExaminationsJournalAsync(ExecuteExaminationsJournalQueryRequest request);

        /// <summary>
        /// Добавляет периодический медицинских осмотр
        /// </summary>
        /// <param name="request">Запрос для добавления периодического медосмотра</param>
        /// <returns></returns>
        Task<PeriodicMedicalExaminationResponse> CreatePeriodicMedicalExaminationAsync(CreatePeriodicMedicalExaminationRequest request);

        /// <summary>
        /// Предоставляет статистику медицинских осмотров
        /// </summary>
        /// <param name="request">Запрос для получения статистики медосмотров</param>
        /// <returns></returns>
        Task<ExaminationsStatisticsResponse> CalculateStatistics(CalculateStatisticsRequest request);

        /// <summary>
        /// Предоставляет медицинское заключение предварительного медосмотра с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор предварительного медосмотра</param>
        /// <returns></returns>
        Task<FileResultResponse> GetPreliminaryExaminationMedicalReportAsync(int examinationId);

        /// <summary>
        /// Предоставляет медицинское заключение периодического медосмотра работника с идентификатором <paramref name="checkupStatusId"/>
        /// </summary>
        /// <param name="checkupStatusId">Идентификатор периодического медосмотра работника</param>
        /// <returns></returns>
        Task<FileResultResponse> GetContingentCheckupStatusMedicalReportAsync(int checkupStatusId);

        /// <summary>
        /// Предоставляет все медицинские заключения периодического медицинского осмотра 
        /// с идентификатором <paramref name="periodicExaminationId"/>
        /// </summary>
        /// <param name="periodicExaminationId">Идентификатор периодического медосмотра</param>
        /// <returns></returns>
        Task<FileResultResponse> GetAllMedicalReportsAsync(int periodicExaminationId);

        /// <summary>
        /// Предоставляет выписку по результатам предварительного медосмотра с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор предварительного медосмотра</param>
        /// <returns></returns>
        Task<FileResultResponse> GetPreliminaryExaminationExcerptAsync(int examinationId);

        /// <summary>
        /// Предоставляет выписку по результатам периодического медосмотра работника с идентификатором <paramref name="checkupStatusId"/>
        /// </summary>
        /// <param name="checkupStatusId">Идентификатор периодического медосмотра работника</param>
        /// <returns></returns>
        Task<FileResultResponse> GetContingentCheckupStatusExcerptAsync(int checkupStatusId);

        /// <summary>
        /// Предоставляет все выписки по результатам периодического осмотра с идентификатором <paramref name="periodicExaminationId"/>
        /// </summary>
        /// <param name="periodicExaminationId">Идентификатор периодического медицинского осмотра</param>
        /// <returns></returns>
        Task<FileResultResponse> GetAllExcerptsAsync(int periodicExaminationId);

        /// <summary>
        /// Предоставляет заключительный акт периодического медосмотра с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор периодического медосмотра</param>
        /// <returns></returns>
        Task<FileResultResponse> GetPeriodicMedicalExaminationReportAsync(int examinationId);

        /// <summary>
        /// Предоставляет годовой отчет по периодическим медосмотрам
        /// </summary>
        /// <param name="request">Запрос для годового отчета по периодическим медосмотрам</param>
        /// <returns></returns>
        Task<FileResultResponse> GetPeriodicMedicalExaminationsYearReport(PeriodicExaminationYearReportRequest request);
    }
}
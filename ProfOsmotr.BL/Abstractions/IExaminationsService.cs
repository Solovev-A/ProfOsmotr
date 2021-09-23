using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IExaminationsService
    {
        Task<PreliminaryMedicalExaminationResponse> CreatePreliminaryMedicalExaminationAsync(CreatePreliminaryMedicalExaminationRequest request);

        Task<PreliminaryMedicalExaminationResponse> DeletePreliminaryExaminationAsync(int examinationId);

        Task<PreliminaryMedicalExaminationResponse> GetPreliminaryMedicalExaminationAsync(int id);

        Task<int> GetPreliminaryMedicalExaminationClinicIdAsync(int examinationId);

        Task<QueryResponse<PreliminaryMedicalExamination>> ListActualPreliminaryMedicalExaminationsAsync(int clinicId);

        Task<QueryResponse<PreliminaryMedicalExamination>> ListPreliminaryMedicalExaminationsAsync(ExecutePreliminaryExaminationsQueryRequest request);

        Task<PreliminaryMedicalExaminationResponse> UpdatePreliminaryExaminationAsync(UpdatePreliminaryExaminationRequest request);

        Task<QueryResponse<PeriodicMedicalExamination>> ListPeriodicMedicalExaminationsAsync(ExecuteQueryBaseRequest request);

        Task<QueryResponse<PeriodicMedicalExamination>> ListActualPeriodicMedicalExaminationsAsync(int clinicId);

        Task<PeriodicMedicalExaminationResponse> GetPeriodicMedicalExaminationAsync(int id);

        Task<int> GetPeriodicMedicalExaminationClinicIdAsync(int examinationId);

        Task<PeriodicMedicalExaminationResponse> DeletePeriodicExaminationAsync(int id);

        Task<ContingentCheckupStatusResponse> CreateContingentCheckupStatus(CreateContingentCheckupStatusRequest request);

        Task<PeriodicMedicalExaminationResponse> UpdatePeriodicExaminationAsync(UpdatePeriodicExaminationRequest request);

        Task<ContingentCheckupStatusResponse> GetContingentCheckupStatus(int id);

        Task<int> GetContingentCheckupStatusClinicIdAsync(int checkupStatusId);

        Task<ContingentCheckupStatusResponse> UpdateContingentCheckupStatusAsync(UpdateContingentCheckupStatusRequest request);

        Task<ContingentCheckupStatusResponse> DeleteContingentCheckupStatusAsync(int id);

        Task<QueryResponse<PreliminaryMedicalExamination>> GetPreliminaryMedicalExaminationsJournalAsync(ExecuteExaminationsJournalQueryRequest request);
        Task<QueryResponse<PeriodicMedicalExamination>> GetPeriodicMedicalExaminationsJournalAsync(ExecuteExaminationsJournalQueryRequest request);
    }
}
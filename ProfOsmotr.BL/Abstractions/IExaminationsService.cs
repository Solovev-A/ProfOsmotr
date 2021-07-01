using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IExaminationsService
    {
        Task<PreliminaryMedicalExaminationResponse> CreatePreliminaryMedicalExaminationAsync(CreatePreliminaryMedicalExaminationRequest request);

        Task<PreliminaryMedicalExaminationResponse> DeleteExaminationAsync(int examinationId);

        Task<PreliminaryMedicalExaminationResponse> GetPreliminaryMedicalExaminationAsync(int id);

        Task<int> GetPreliminaryMedicalExaminationClinicIdAsync(int examinationId);

        Task<QueryResponse<PreliminaryMedicalExamination>> ListActualPreliminaryMedicalExaminationsAsync(int clinicId);

        Task<QueryResponse<PreliminaryMedicalExamination>> ListPreliminaryMedicalExaminationsAsync(ExecutePreliminaryExaminationsQueryRequest request);
    }
}
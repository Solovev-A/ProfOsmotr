using ProfOsmotr.BL;
using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IPatientService
    {
        Task<PatientResponse> CreatePatient(CreatePatientRequest request);
        Task<PatientResponse> DeletePatientAsync(int id);
        Task<PatientResponse> GetPatientAsync(int id);
        Task<QueryResponse<Patient>> ListPatientsAsync(ListPatientsRequest request);
        Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request);
    }
}

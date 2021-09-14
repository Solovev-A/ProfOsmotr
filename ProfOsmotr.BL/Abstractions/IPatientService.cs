﻿using ProfOsmotr.BL;
using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IPatientService
    {
        Task<PatientResponse> CheckPatientAsync(int id);
        Task<PatientResponse> CreatePatient(CreatePatientRequest request);
        Task<PatientResponse> DeletePatientAsync(int id);
        Task<PatientSearchResultResponse> FindPatientWithSuggestions(FindPatientWithSuggestionsRequest request);
        Task<PatientResponse> GetPatientAsync(int id);
        Task<QueryResponse<Patient>> ListActualPatientsAsync(int clinicId);
        Task<QueryResponse<Patient>> ListPatientsAsync(ListPatientsRequest request);
        Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request);
    }
}

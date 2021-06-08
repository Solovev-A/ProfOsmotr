﻿using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class PatientService : IPatientService
    {
        private readonly IProfUnitOfWork uow;
        private readonly IClinicService clinicService;
        private readonly IMapper mapper;

        public PatientService(IProfUnitOfWork uow, IClinicService clinicService, IMapper mapper)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PatientResponse> GetPatientAsync(int id)
        {
            var patient = await uow.Patients.FindPatientAsync(id);

            if (patient is null)
            {
                return new PatientResponse("Пациент не найден");
            }

            return new PatientResponse(patient);
        }

        public async Task<QueryResponse<Patient>> ListPatientsAsync(ListPatientsRequest request)
        {
            int start = (request.Page - 1) * request.ItemsPerPage;
            try
            {
                var result = await uow.Patients.ExecuteQuery(start,
                                                             request.ItemsPerPage,
                                                             request.Search,
                                                             patient => patient.ClinicId == request.ClinicId);
                return new QueryResponse<Patient>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<Patient>(ex.Message);
            }
        }

        public async Task<PatientResponse> CreatePatient(CreatePatientRequest request)
        {
            var clinicResponse = await clinicService.GetClinic(request.ClinicId);
            if (!clinicResponse.Succeed)
            {
                return new PatientResponse(clinicResponse.Message);
            }

            Patient patient = mapper.Map<Patient>(request);

            try
            {
                await uow.Patients.AddAsync(patient);
                await uow.SaveAsync();
                return new PatientResponse(patient);
            }
            catch (Exception ex)
            {
                return new PatientResponse(ex.Message);
            }
        }

        public async Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request)
        {
            var patient = await uow.Patients.FindAsync(request.PatientId);

            if (patient is null)
            {
                return new PatientResponse("Пациент не найден");
            }

            mapper.Map(request.Query, patient);

            try
            {
                await uow.SaveAsync();
                return new PatientResponse(patient);
            }
            catch (Exception ex)
            {
                return new PatientResponse(ex.Message);
            }
        }

        public async Task<PatientResponse> DeletePatientAsync(int id)
        {
            var patient = await uow.Patients.FindAsync(id);

            if (patient is null)
            {
                return new PatientResponse("Пациент не найден");
            }

            if (patient.IndividualCheckupStatuses.Any() || patient.ContingentCheckupStatuses.Any())
            {
                return new PatientResponse("Невозможно удалить пациента, для которого зарегистрированы медицинские осмотры");
            }

            try
            {
                uow.Patients.Delete(id);
                await uow.SaveAsync();
                return new PatientResponse(patient);
            }
            catch (Exception ex)
            {
                return new PatientResponse(ex.Message);
            }
        }
    }
}
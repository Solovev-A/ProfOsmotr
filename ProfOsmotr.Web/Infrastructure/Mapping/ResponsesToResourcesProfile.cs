using AutoMapper;
using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.Web.Infrastructure.Mapping
{
    public class ResponsesToResourcesProfile : Profile
    {
        const string DATE_FORMAT_FOR_DISPLAY = "dd.MM.yyyy";

        public ResponsesToResourcesProfile()
        {
            CreateMap<CalculationResultItem, CalculationResultItemResource>()
                .ForMember(d => d.Code, config => config.MapFrom(s => s.Service.ServiceDetails.Code))
                .ForMember(d => d.ExaminationName, config => config.MapFrom(s => s.Service.OrderExamination.Name))
                .ForMember(d => d.FullName, config => config.MapFrom(s => s.Service.ServiceDetails.FullName))
                .ForMember(d => d.Price, config => config.MapFrom(s => s.Service.Price))
                .ForMember(d => d.ServiceAvailabilityGroupId, config => config.MapFrom(s => s.Service.ServiceAvailabilityGroupId));

            CreateMap<CalculationSource, CalculationSourceResource>()
                .ForMember(d => d.Profession, config => config.MapFrom(d => d.Profession.Name))
                .ForMember(d => d.OrderItems, config => config
                                              .MapFrom(d => d.Profession.OrderItems
                                                                .Select(item => GetFullItemKey(item))));

            CreateMap<Calculation, CalculationResource>()
                .ForMember(d => d.Sources, conf => conf.MapFrom(s => s.CalculationSources))
                .ForMember(d => d.Results, conf => conf.MapFrom(s => s.CalculationResultItems
                                                                        .OrderBy(i => i.Service.OrderExamination.Name)));

            CreateMap<Calculation, CalculationGeneralDataResource>()
                .Include<Calculation, CalculationResource>()
                .ForMember(d => d.CreatorName, conf => conf.MapFrom(s => s.Creator.UserProfile.Name))
                .ForMember(d => d.CreatorPosition, conf => conf.MapFrom(s => s.Creator.UserProfile.Position));

            CreateMap<PaginatedResult<Calculation>, PaginatedResource<CalculationGeneralDataResource>>();

            CreateMap<Service, CatalogItemResource>()
                .ForMember(d => d.Code, conf => conf.MapFrom(s => s.ServiceDetails.Code))
                .ForMember(d => d.FullName, conf => conf.MapFrom(s => s.ServiceDetails.FullName))
                .ForMember(d => d.ServiceAvailabilityGroupName, conf => conf.MapFrom(s => s.ServiceAvailabilityGroup.Name))
                .ForMember(d => d.OrderExaminationName, conf => conf.MapFrom(s => s.OrderExamination.Name));

            CreateMap<OrderExamination, OrderExaminationResource>();

            CreateMap<OrderExamination, OrderExaminationDetailedResource>();

            CreateMap<ServiceDetails, ServiceDetailsResource>();

            CreateMap<TargetGroup, TargetGroupResource>();

            CreateMap<OrderItem, OrderItemResource>();

            CreateMap<OrderItem, OrderItemDetailedResource>()
                .ForMember(d => d.OrderExaminations, conf => conf.MapFrom(s => s.OrderExaminations.Select(x => x.Id)));

            CreateMap<ClinicRegisterRequest, RegisterRequestResource>();

            CreateMap<User, RequestSenderResource>()
                .ForMember(d => d.Name, conf => conf.MapFrom(s => s.UserProfile.Name))
                .ForMember(d => d.Position, conf => conf.MapFrom(s => s.UserProfile.Position));

            CreateMap<Clinic, ClinicResource>();

            CreateMap<ClinicDetails, ClinicDetailsResource>();

            CreateMap<User, UsersListItem>()
                .Include<User, UsersListGlobalItem>()
                .ForMember(d => d.Name, conf => conf.MapFrom(s => s.UserProfile.Name))
                .ForMember(d => d.Position, conf => conf.MapFrom(s => s.UserProfile.Position));

            CreateMap<User, UsersListGlobalItem>()
                .ForMember(d => d.ClinicShortName, conf => conf.MapFrom(s => s.Clinic.ClinicDetails.ShortName));

            CreateMap<Role, RoleResource>();

            CreateMap<ExaminationResultIndex, ExaminationResultIndexResource>();

            CreateMap<Patient, PatientResource>()
                .ForMember(d => d.DateOfBirth, conf => conf.MapFrom(s => s.DateOfBirth.ToString(DATE_FORMAT_FOR_DISPLAY)))
                .ForMember(d => d.Gender, conf => conf.MapFrom(s => s.GenderId.ToString().ToLower()))
                .ForMember(d => d.PreliminaryMedicalExaminations, conf => conf.MapFrom(s => s.IndividualCheckupStatuses));


            CreateMap<CheckupStatus, PatientCheckupStatusListItemResource>()
                .ForMember(d => d.Result, conf => conf.MapFrom(s => s.CheckupResultId.HasValue
                                                                  ? s.CheckupResultId.DisplayName()
                                                                  : null))
                .ForMember(d => d.DateOfCompletion, conf => conf.MapFrom(s => ToString(s.DateOfCompletion)))
                .ForMember(d => d.Profession, conf => conf.MapFrom(s => s.Profession.Name))
                .ForMember(d => d.OrderItems, conf => conf.MapFrom(s => s.Profession.OrderItems.Select(oi => oi.Key)));

            CreateMap<IndividualCheckupStatus, PatientCheckupStatusListItemResource>()
                .IncludeBase<CheckupStatus, PatientCheckupStatusListItemResource>()
                .ForMember(d => d.Id, conf => conf.MapFrom(s => s.PreliminaryMedicalExaminationId))
                .ForMember(d => d.Employer, conf => conf.MapFrom(s => s.PreliminaryMedicalExamination.Employer.Name));

            CreateMap<ContingentCheckupStatus, PatientCheckupStatusListItemResource>()
                .IncludeBase<CheckupStatus, PatientCheckupStatusListItemResource>()
                .ForMember(d => d.Employer, conf => conf.MapFrom(s => s.PeriodicMedicalExamination.Employer.Name));

            CreateMap(typeof(QueryResult<>), typeof(PagedResource<>));

            CreateMap<Patient, PatientsListItemResource>()
                .ForMember(d => d.DateOfBirth, conf => conf.MapFrom(s => s.DateOfBirth.ToString(DATE_FORMAT_FOR_DISPLAY)));

            CreateMap<Employer, EmployerListItemResource>();

            CreateMap<Employer, EmployerResource>();

            CreateMap<PeriodicMedicalExamination, BasePeriodicExaminationListItemResource>()
                .ForMember(d => d.IsCompleted, conf => conf.MapFrom(s => s.Completed))
                .ForMember(d => d.ReportDate, conf => conf.MapFrom(s => ToString(s.ReportDate)));

            CreateMap<PeriodicMedicalExamination, EmployerPeriodicMedicalExaminationResource>()
                .IncludeBase<PeriodicMedicalExamination, BasePeriodicExaminationListItemResource>();

            CreateMap<PreliminaryMedicalExamination, EmployerPreliminaryMedicalExaminationResource>()
                .ForMember(d => d.Patient, conf => conf.MapFrom(s => GetFullName(s.CheckupStatus.Patient)))
                .ForMember(d => d.ReportDate, conf => conf.MapFrom(s => ToString(s.CheckupStatus.DateOfCompletion)));

            CreateMap<PreliminaryMedicalExamination, PreliminaryMedicalExaminationsListItemResource>()
                .ForMember(d => d.Profession, conf => conf.MapFrom(s => s.CheckupStatus.Profession.Name))
                .ForMember(d => d.OrderItems, conf => conf.MapFrom(s => s.CheckupStatus.Profession.OrderItems.Select(oi => oi.Key)))
                .ForMember(d => d.DateOfCompletion, conf => conf.MapFrom(s => ToString(s.CheckupStatus.DateOfCompletion)))
                .ForMember(d => d.EmployerName, conf => conf.MapFrom(s => s.Employer.Name))
                .ForMember(d => d.IsCompleted, conf => conf.MapFrom(s => s.Completed))
                .ForMember(d => d.Patient, conf => conf.MapFrom(s => s.CheckupStatus.Patient));

            CreateMap<PreliminaryMedicalExamination, PreliminaryMedicalExaminationResource>()
                .ForMember(d => d.CheckupExaminationResultIndexes, conf => conf.MapFrom(s => MapToCheckupExaminationResultIndexResources(s.CheckupStatus.IndividualCheckupIndexValues)))
                .ForMember(d => d.DateOfComplition, conf => conf.MapFrom(s => ToString(s.CheckupStatus.DateOfCompletion)))
                .ForMember(d => d.MedicalReport, conf => conf.MapFrom(s => s.CheckupStatus.MedicalReport))
                .ForMember(d => d.Patient, conf => conf.MapFrom(s => s.CheckupStatus.Patient))
                .ForMember(d => d.RegistrationJournalEntryNumber, conf => conf.MapFrom(s => s.CheckupStatus.RegistrationJournalEntryNumber))
                .ForMember(d => d.Result, conf => conf.MapFrom(s => s.CheckupStatus.CheckupResult))
                .ForPath(d => d.WorkPlace.Profession, conf => conf.MapFrom(s => s.CheckupStatus.Profession))
                .ForPath(d => d.WorkPlace.Employer, conf => conf.MapFrom(s => s.Employer))
                .ForPath(d => d.WorkPlace.Employer.Department, conf => conf.MapFrom(s => s.CheckupStatus.EmployerDepartment))
                .AfterMap((s, d) =>
                {
                    if (s.Employer is null)
                        d.WorkPlace.Employer = null;
                    // чтобы не оставался объект с id: 0
                });

            CreateMap<Employer, PreliminaryExaminationEmployerResource>();

            CreateMap<EmployerDepartment, EmployerDepartmentResource>();

            CreateMap<IndividualCheckupIndexValue, CheckupIndexValueResource>()
                .ForMember(d => d.Index, conf => conf.MapFrom(s => s.ExaminationResultIndex));

            CreateMap<Patient, ExaminationPatientResource>()
                .ForMember(d => d.DateOfBirth, conf => conf.MapFrom(s => s.DateOfBirth.ToString(DATE_FORMAT_FOR_DISPLAY)));

            CreateMap<User, ExaminationEditorResource>()
                .ForMember(d => d.FullName, conf => conf.MapFrom(s => s.UserProfile.Name))
                .ForMember(d => d.Position, conf => conf.MapFrom(s => s.UserProfile.Position));

            CreateMap<CheckupResult, CheckupResultResource>()
                .ForMember(d => d.Id, conf => conf.MapFrom(s => s.Id.ToString()));

            CreateMap<PreliminaryMedicalExamination, CreatedPreliminaryExaminationResource>();

            CreateMap<ProfessionSearchResult, ProfessionSearchResultResource>();

            CreateMap<Profession, ProfessionResource>()
                .ForMember(d => d.OrderItems, conf => conf.MapFrom(s => s.OrderItems.Select(i => i.Key)));

            CreateMap<PeriodicMedicalExamination, PeriodicMedicalExaminationListItemResource>()
                .IncludeBase<PeriodicMedicalExamination, BasePeriodicExaminationListItemResource>()
                .ForMember(d => d.Employer, conf => conf.MapFrom(s => s.Employer.Name));
        }

        private string GetFullItemKey(OrderItem item)
        {
            return $"п. {item.Key}";
        }

        private string GetFullName(Patient patient)
        {
            return $"{patient.LastName} {patient.FirstName} {patient.PatronymicName ?? string.Empty}";
        }

        private IEnumerable<CheckupExaminationResultIndexResource> MapToCheckupExaminationResultIndexResources(
            IEnumerable<IndividualCheckupIndexValue> values)
        {
            return values
                .GroupBy(iv => iv.ExaminationResultIndex.OrderExamination.Name)
                .Select(g => new CheckupExaminationResultIndexResource()
                {
                    ExaminationName = g.Key,
                    CheckupIndexValues = g.Select(iv => new CheckupIndexValueResource()
                    {
                        Value = iv.Value,
                        Index = new ExaminationResultIndexResource()
                        {
                            Id = iv.ExaminationResultIndex.Id,
                            Title = iv.ExaminationResultIndex.Title,
                            UnitOfMeasure = iv.ExaminationResultIndex.UnitOfMeasure
                        }
                    })
                });
        }

        private string ToString(DateTime? dateTime)
        {
            return dateTime.HasValue
                ? dateTime.Value.ToString("dd.MM.yyyy")
                : null;
        }
    }
}
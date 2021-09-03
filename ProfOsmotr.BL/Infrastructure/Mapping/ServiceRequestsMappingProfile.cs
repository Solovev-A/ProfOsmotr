using AutoMapper;
using ProfOsmotr.Hashing;
using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL.Infrastructure.Mapping
{
    public class ServiceRequestsMappingProfile : Profile
    {
        public ServiceRequestsMappingProfile()
        {
            CreateMap<Clinic, ClinicRegisterRequest>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.ClinicDetails.Address))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.ClinicDetails.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.ClinicDetails.FullName))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.ClinicDetails.Phone))
                .ForMember(d => d.ShortName, opt => opt.MapFrom(s => s.ClinicDetails.ShortName))
                .ReverseMap()
                .ForPath(x => x.Id, x => x.Ignore());

            CreateMap<RegisterDataRequest, ClinicRegisterRequest>();

            CreateMap<OrderExamination, SaveOrderExaminationRequest>()
                .ForMember(d => d.DefaultServiceCode, opt => opt.MapFrom(s => s.DefaultServiceDetails.Code))
                .ForMember(d => d.DefaultServiceFullName, opt => opt.MapFrom(s => s.DefaultServiceDetails.FullName))
                .ReverseMap();

            CreateMap<AddOrderItemRequest, OrderItem>();

            //CreateMap<User, UpdateUserRequest>()
            //    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.UserProfile.Name))
            //    .ForMember(d => d.Position, opt => opt.MapFrom(s => s.UserProfile.Position))
            //    .ReverseMap()
            //    .ForPath(x => x.RoleId, x => x.Ignore())
            //    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            // пропускает значения null в свойства UserProfile

            CreateMap<UpdateUserRequest, User>()
                .ForPath(x => x.RoleId, x => x.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateClinicDetailsRequest, ClinicDetails>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SaveExaminationResultIndexRequest, ExaminationResultIndex>();

            CreateMap<CreatePatientRequest, Patient>()
                .ForMember(d => d.Gender, conf => conf.Ignore())
                .ForMember(d => d.GenderId, conf => conf.MapFrom(s => s.Gender));

            CreateMap<PatchPatientQuery, Patient>()
                .ForMember(d => d.Gender, conf => conf.Ignore())
                .ForMember(d => d.Address, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.Address))))
                .ForMember(d => d.FirstName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.FirstName))))
                .ForMember(d => d.LastName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.LastName))))
                .ForMember(d => d.PatronymicName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.PatronymicName))))
                .ForMember(d => d.DateOfBirth, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.DateOfBirth))))
                .ForMember(d => d.GenderId, conf =>
                {
                    conf.Condition(s => s.IsFieldPresent(nameof(s.Gender)));
                    conf.MapFrom(s => s.Gender);
                });

            CreateMap<CreateEmployerRequest, Employer>();

            CreateMap<PatchEmployerQuery, Employer>()
                .ForMember(d => d.Name, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.Name))))
                .ForMember(d => d.HeadFirstName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.HeadFirstName))))
                .ForMember(d => d.HeadLastName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.HeadLastName))))
                .ForMember(d => d.HeadPatronymicName, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.HeadPatronymicName))))
                .ForMember(d => d.HeadPosition, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.HeadPosition))));

            CreateMap<CreateEmployerDepartmentRequest, EmployerDepartment>();

            CreateMap<PatchEmployerDepartmentQuery, EmployerDepartment>()
                .ForMember(d => d.Name, conf => conf.Condition(s => s.IsFieldPresent(nameof(s.Name))));
        }
    }
}

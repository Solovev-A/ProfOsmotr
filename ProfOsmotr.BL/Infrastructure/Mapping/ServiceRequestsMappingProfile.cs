using AutoMapper;
using ProfOsmotr.Hashing;
using ProfOsmotr.DAL;

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

            CreateMap<AddOrderItemRequest, OrderItem>()
                .ForMember(d => d.OrderAnnexId, conf => conf.Ignore());

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
        }
    }
}

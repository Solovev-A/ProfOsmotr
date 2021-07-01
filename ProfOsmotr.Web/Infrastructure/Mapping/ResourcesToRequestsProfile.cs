using AutoMapper;
using ProfOsmotr.BL;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Models;

namespace ProfOsmotr.Web.Infrastructure.Mapping
{
    public class ResourcesToRequestsProfile : Profile
    {
        public ResourcesToRequestsProfile()
        {
            CreateMap<CreateProfessionResource, CreateProfessionRequest>()
                .ForMember(d => d.OrderItemIdentifiers, conf => conf.MapFrom(s => s.OrderItems));

            CreateMap<CreateCalculationResource, CreateCalculationRequest>()
                .ForMember(d => d.CreateProfessionRequests, conf => conf.MapFrom(s => s.Professions));

            CreateMap<UpdateCalculationResource, UpdateCalculationRequest>();

            CreateMap<UpdateCalculationResultItemResource, UpdateCalculationResultItemRequest>();

            CreateMap<PaginationQueryResource, CalculationsPaginationQuery>();

            CreateMap<SaveServiceRequest, UpdateCatalogItemResource>()
                .ForMember(d => d.FullName, conf => conf.MapFrom(s => s.ServiceDetails.FullName))
                .ForMember(d => d.Code, conf => conf.MapFrom(s => s.ServiceDetails.Code))
                .ReverseMap();

            CreateMap<AddOrderItemResource, AddOrderItemRequest>()
                .ForMember(d => d.OrderExaminationIdentifiers, conf => conf.MapFrom(s => s.Examinations));

            CreateMap<UpdateOrderItemResource, UpdateOrderItemRequest>()
                .ForMember(d => d.OrderExaminationIdentifiers, conf => conf.MapFrom(s => s.Examinations));

            CreateMap<UpdateOrderExaminationResource, SaveOrderExaminationRequest>();

            CreateMap<AddOrderExaminationResource, SaveOrderExaminationRequest>();

            CreateMap<RegisterRequestSenderResource, CreateUserRequest>();

            CreateMap<CreateUserResource, CreateUserRequest>();

            CreateMap<UpdateUserResource, UpdateUserRequest>();

            CreateMap<CreateRegisterRequestResource, RegisterDataRequest>();

            CreateMap<ManageClinicResource, ManageClinicRequest>();

            CreateMap<UpdateClinicDetailsResource, UpdateClinicDetailsRequest>();

            CreateMap<SaveExaminationResultIndexResource, SaveExaminationResultIndexRequest>();

            CreateMap<SearchPaginationQuery, ListPatientsRequest>();

            CreateMap<CreatePatientQuery, CreatePatientRequest>();

            CreateMap<SearchPaginationQuery, ExecuteQueryBaseRequest>();

            CreateMap<CreateEmployerQuery, CreateEmployerRequest>();

            CreateMap<PreliminaryExaminationSearchPaginationQuery, ExecutePreliminaryExaminationsQueryRequest>();
        }
    }
}
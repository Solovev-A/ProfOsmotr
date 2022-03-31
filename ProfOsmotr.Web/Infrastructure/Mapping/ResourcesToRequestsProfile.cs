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
            CreateMap<CreateCalculationSourceQuery, CreateCalculationSourceRequest>();

            CreateMap<CreateProfessonQuery, CreateProfessionRequest>()
                .ForMember(d => d.OrderItemIdentifiers, conf => conf.MapFrom(s => s.OrderItems));

            CreateMap<CreateCalculationQuery, CreateCalculationRequest>()
                .ForMember(d => d.CreateCalculationSourceRequests, conf => conf.MapFrom(s => s.Sources));

            CreateMap<UpdateCalculationQuery, UpdateCalculationRequest>();

            CreateMap<UpdateCalculationResultItemQuery, UpdateCalculationResultItemRequest>();

            CreateMap<BasePaginationQuery, CalculationsPaginationQuery>();

            CreateMap<SaveServiceRequest, UpdateCatalogItemQuery>()
                .ForMember(d => d.FullName, conf => conf.MapFrom(s => s.ServiceDetails.FullName))
                .ForMember(d => d.Code, conf => conf.MapFrom(s => s.ServiceDetails.Code))
                .ReverseMap();

            CreateMap<AddOrderItemQuery, AddOrderItemRequest>()
                .ForMember(d => d.OrderExaminationIdentifiers, conf => conf.MapFrom(s => s.Examinations));

            CreateMap<UpdateOrderItemQuery, UpdateOrderItemRequest>()
                .ForMember(d => d.OrderExaminationIdentifiers, conf => conf.MapFrom(s => s.Examinations));

            CreateMap<UpdateOrderExaminationQuery, SaveOrderExaminationRequest>();

            CreateMap<AddOrderExaminationQuery, SaveOrderExaminationRequest>();

            CreateMap<RegisterRequestSenderQuery, CreateUserRequest>();

            CreateMap<CreateUserResource, CreateUserRequest>();

            CreateMap<UpdateUserQuery, UpdateUserRequest>();

            CreateMap<CreateRegisterRequestQuery, RegisterDataRequest>();

            CreateMap<ManageClinicQuery, ManageClinicRequest>();

            CreateMap<UpdateClinicDetailsQuery, UpdateClinicDetailsRequest>();

            CreateMap<SaveExaminationResultIndexQuery, SaveExaminationResultIndexRequest>();

            CreateMap<SearchPatientQuery, ListPatientsRequest>();

            CreateMap<SearchPatientQuery, FindPatientWithSuggestionsRequest>();

            CreateMap<CreatePatientQuery, CreatePatientRequest>();

            CreateMap<SearchPaginationQuery, ExecuteQueryBaseRequest>();

            CreateMap<CreateEmployerQuery, CreateEmployerRequest>();

            CreateMap<PreliminaryExaminationSearchPaginationQuery, ExecutePreliminaryExaminationsQueryRequest>();

            CreateMap<SearchProfessionQuery, FindProfessionRequest>();

            CreateMap<CreateEmployerDepartmentQuery, CreateEmployerDepartmentRequest>();

            CreateMap<CreateContingentCheckupStatusQuery, CreateContingentCheckupStatusRequest>();

            CreateMap<ExaminationJournalQuery, ExecuteExaminationsJournalQueryRequest>();

            CreateMap<CreatePeriodicExaminationQuery, CreatePeriodicMedicalExaminationRequest>();
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProfOsmotr.Hashing;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.Web.Services;

namespace ProfOsmotr.Web.Infrastructure
{
    public static class ServicesExtensions
    {
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(conf => conf.AddDebug());

        public static void AddProfOsmotr(this IServiceCollection services, string connectionString, bool isAuthorizationEnabled)
        {
            AddAutoMapper(services);
            AddDatabase(services, connectionString);
            AddRepositories(services);
            AddAppServices(services, isAuthorizationEnabled);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            //mapping profiles
            var p1 = typeof(Infrastructure.Mapping.ResponsesToResourcesProfile);
            var p2 = typeof(BL.Infrastructure.Mapping.ServiceRequestsMappingProfile);
            services.AddAutoMapper(p1, p2);
        }

        private static void AddDatabase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProfContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlite(connectionString));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<OrderDataConfiguration>();
            services.AddTransient<DataSeeder>();
            services.AddTransient<IProfUnitOfWork, ProfEFUnitOfWork>();
            services.AddTransient<ICalculationRepository, CalculationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IRepository<Profession>, EFRepository<Profession>>();
            services.AddTransient<IRepository<Service>, EFRepository<Service>>();
            services.AddTransient<IRepository<ServiceAvailabilityGroup>, EFRepository<ServiceAvailabilityGroup>>();
            services.AddTransient<IOrderExaminationRepository, OrderExaminationRepository>();
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IQueryAwareRepository<Clinic>, ClinicRepository>();
            services.AddTransient<IQueryAwareRepository<User>, UserRepository>();
            services.AddTransient<IQueryAwareRepository<ClinicRegisterRequest>, ClinicRegisterRequestRepository>();
            services.AddTransient<IRepository<Role>, EFRepository<Role>>();
            services.AddTransient<IRepository<TargetGroup>, EFRepository<TargetGroup>>();
            services.AddTransient<IRepository<ICD10Chapter>, EFRepository<ICD10Chapter>>();
            services.AddTransient<IRepository<Gender>, EFRepository<Gender>>();
            services.AddTransient<IRepository<CheckupResult>, EFRepository<CheckupResult>>();
            services.AddTransient<IRepository<ExaminationResultIndex>, EFRepository<ExaminationResultIndex>>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<IPreliminaryMedicalExaminationRepository, PreliminaryMedicalExaminationRepository>();
            services.AddTransient<IProfessionRepository, ProfessionRepository>();
            services.AddTransient<IRepository<EmployerDepartment>, EFRepository<EmployerDepartment>>();
            services.AddTransient<IPeriodicMedicalExaminationRepository, PeriodicMedicalExaminationRepository>();
        }

        private static void AddAppServices(IServiceCollection services, bool isAuthorizationEnabled)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICalculationService, CalculationService>();
            services.AddTransient<ICalculationSourceFactory, CalculationSourceFactory>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IClinicService, ClinicService>();
            services.AddTransient<IProfCalculator, ProfCalculator>();
            services.AddTransient<IProfessionService, ProfessionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthenticationService>();
            if (isAuthorizationEnabled)
            {
                services.AddTransient<IAccessService, AccessService>();
            }
            else
            {
                services.AddTransient<IAccessService, DevelopmentAccessService>();
            }
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IQueryHandler, ApiQueryHandler>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IExaminationsService, ExaminationsService>();
            services.AddTransient<IICD10Service, ICD10Service>();
            services.AddTransient<IReportsCreator, DocxReportsCreator>();
        }
    }
}
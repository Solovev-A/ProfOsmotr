﻿using AutoMapper;
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

        public static void AddProfOsmotr(this IServiceCollection services, string connectionString)
        {
            AddAutoMapper(services);
            AddDatabase(services, connectionString);
            AddRepositories(services);
            AddAppServices(services);
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
            services.AddTransient<IOrderReposytory, OrderRepository>();
            services.AddTransient<IRepository<Profession>, EFRepository<Profession>>();
            services.AddTransient<IRepository<Service>, EFRepository<Service>>();
            services.AddTransient<IRepository<ServiceAvailabilityGroup>, EFRepository<ServiceAvailabilityGroup>>();
            services.AddTransient<IRepository<OrderExamination>, EFRepository<OrderExamination>>();
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IQueryAwareRepository<Clinic>, ClinicRepository>();
            services.AddTransient<IQueryAwareRepository<User>, UserRepository>();
            services.AddTransient<IQueryAwareRepository<ClinicRegisterRequest>, ClinicRegisterRequestRepository>();
            services.AddTransient<IRepository<OrderAnnex>, EFRepository<OrderAnnex>>();
            services.AddTransient<IRepository<Role>, EFRepository<Role>>();
            services.AddTransient<IRepository<TargetGroup>, EFRepository<TargetGroup>>();
        }

        private static void AddAppServices(IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICalculationService, CalculationService>();
            services.AddTransient<ICalculationSourceFactory, CalculationSourceFactory>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IClinicService, ClinicService>();
            services.AddTransient<IProfCalculator, Calculator302n>();
            services.AddTransient<IProfessionService, ProfessionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthenticationService>();
            services.AddTransient<IAccessService, AccessService>();
        }
    }
}
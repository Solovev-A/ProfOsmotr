using ProfOsmotr.DAL.Abstractions;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class ProfEFUnitOfWork : IProfUnitOfWork
    {
        #region Fields

        private readonly ProfContext context;
        private bool disposedValue = false;

        #endregion Fields

        #region Constructors

        public ProfEFUnitOfWork(
            ProfContext context,
            ICatalogRepository actualClinicServices,
            ICalculationRepository calculations,
            IRepository<CheckupResult> checkupResults,
            IQueryAwareRepository<ClinicRegisterRequest> clinicRegisterRequests,
            IQueryAwareRepository<Clinic> clinics,
            IRepository<EmployerDepartment> employerDepartments,
            IEmployerRepository employers,
            IRepository<ExaminationResultIndex> examintaionResultIndexes,
            IRepository<Gender> genders,
            IRepository<ICD10Chapter> iCD10Chapters,
            IOrderExaminationRepository orderExaminations,
            IOrderRepository orderItems,
            IPatientRepository patients,
            IPeriodicMedicalExaminationRepository periodicMedicalExaminations,
            IPreliminaryMedicalExaminationRepository preliminaryMedicalExaminations,
            IProfessionRepository professions,
            IRepository<Role> roles,
            IRepository<ServiceAvailabilityGroup> serviceAvailabilityGroups,
            IRepository<Service> services,
            IRepository<TargetGroup> targetGroups,
            IQueryAwareRepository<User> users)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            ActualClinicServices = actualClinicServices ?? throw new ArgumentNullException(nameof(actualClinicServices));
            Calculations = calculations ?? throw new ArgumentNullException(nameof(calculations));
            CheckupResults = checkupResults ?? throw new ArgumentNullException(nameof(checkupResults));
            ClinicRegisterRequests = clinicRegisterRequests ?? throw new ArgumentNullException(nameof(clinicRegisterRequests));
            Clinics = clinics ?? throw new ArgumentNullException(nameof(clinics));
            EmployerDepartments = employerDepartments ?? throw new ArgumentNullException(nameof(employerDepartments));
            Employers = employers ?? throw new ArgumentNullException(nameof(employers));
            ExamintaionResultIndexes = examintaionResultIndexes ?? throw new ArgumentNullException(nameof(examintaionResultIndexes));
            Genders = genders ?? throw new ArgumentNullException(nameof(genders));
            ICD10Chapters = iCD10Chapters ?? throw new ArgumentNullException(nameof(iCD10Chapters));
            OrderExaminations = orderExaminations ?? throw new ArgumentNullException(nameof(orderExaminations));
            OrderItems = orderItems ?? throw new ArgumentNullException(nameof(orderItems));
            Patients = patients ?? throw new ArgumentNullException(nameof(patients));
            PeriodicMedicalExaminations = periodicMedicalExaminations ?? throw new ArgumentNullException(nameof(periodicMedicalExaminations));
            PreliminaryMedicalExaminations = preliminaryMedicalExaminations ?? throw new ArgumentNullException(nameof(preliminaryMedicalExaminations));
            Professions = professions ?? throw new ArgumentNullException(nameof(professions));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
            ServiceAvailabilityGroups = serviceAvailabilityGroups ?? throw new ArgumentNullException(nameof(serviceAvailabilityGroups));
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TargetGroups = targetGroups ?? throw new ArgumentNullException(nameof(targetGroups));
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }

        #endregion Constructors

        #region Properties

        public ICatalogRepository ActualClinicServices { get; }

        public ICalculationRepository Calculations { get; }

        public IRepository<CheckupResult> CheckupResults { get; }

        public IQueryAwareRepository<ClinicRegisterRequest> ClinicRegisterRequests { get; }

        public IQueryAwareRepository<Clinic> Clinics { get; }

        public IRepository<EmployerDepartment> EmployerDepartments { get; }

        public IEmployerRepository Employers { get; }

        public IRepository<ExaminationResultIndex> ExamintaionResultIndexes { get; }

        public IRepository<Gender> Genders { get; }

        public IRepository<ICD10Chapter> ICD10Chapters { get; }

        public IOrderExaminationRepository OrderExaminations { get; }

        public IOrderRepository OrderItems { get; }

        public IPatientRepository Patients { get; }

        public IPeriodicMedicalExaminationRepository PeriodicMedicalExaminations { get; }

        public IPreliminaryMedicalExaminationRepository PreliminaryMedicalExaminations { get; }

        public IProfessionRepository Professions { get; }

        public IRepository<Role> Roles { get; }

        public IRepository<ServiceAvailabilityGroup> ServiceAvailabilityGroups { get; }

        public IRepository<Service> Services { get; }

        public IRepository<TargetGroup> TargetGroups { get; }

        public IQueryAwareRepository<User> Users { get; }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposedValue = true;
            }
        }

        #endregion Methods
    }
}
using Microsoft.EntityFrameworkCore;

namespace ProfOsmotr.DAL
{
    public class ProfContext : DbContext
    {
        internal DbSet<Calculation> Calculations { get; private set; }

        internal DbSet<OrderItem> OrderItems { get; private set; }

        internal DbSet<Profession> Professions { get; private set; }

        internal DbSet<Service> Services { get; private set; }

        internal DbSet<ServiceAvailabilityGroup> ServiceAvailabilityGroups { get; private set; }

        internal DbSet<CalculationResultItem> CalculationResultItems { get; private set; }

        internal DbSet<OrderExamination> OrderExaminations { get; private set; }

        internal DbSet<Clinic> Clinics { get; private set; }

        internal DbSet<User> Users { get; private set; }

        internal DbSet<ClinicRegisterRequest> ClinicRegisterRequests { get; private set; }

        internal DbSet<ExaminationResultIndex> ExaminationResultIndexes { get; private set; }

        internal DbSet<PreliminaryMedicalExamination> PreliminaryMedicalExaminations { get; private set; }

        internal DbSet<PeriodicMedicalExamination> PeriodicMedicalExaminations { get; private set; }

        internal DbSet<IndividualCheckupStatus> IndividualCheckupStatuses { get; private set; }

        internal DbSet<ContingentCheckupStatus> ContingentCheckupStatuses { get; private set; }

        internal DbSet<Employer> Employers { get; private set; }

        internal DbSet<ICD10Chapter> ICD10Chapters { get; private set; }

        internal DbSet<Patient> Patients { get; private set; }

        public ProfContext(DbContextOptions<ProfContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActualClinicService>(EFConfigure.ActualClinicService);
            modelBuilder.Entity<OrderExamination>(EFConfigure.OrderExamination);
            modelBuilder.Entity<Service>(EFConfigure.Service);
            modelBuilder.Entity<ServiceAvailabilityGroup>(EFConfigure.ServiceAvailabilityGroup);
            modelBuilder.Entity<Calculation>(EFConfigure.Calculation);
            modelBuilder.Entity<User>(EFConfigure.User);
            modelBuilder.Entity<Role>(EFConfigure.Role);
            modelBuilder.Entity<TargetGroup>(EFConfigure.TargetGroup);
            modelBuilder.Entity<ClinicRegisterRequest>(EFConfigure.ClinicRegisterRequest);
            modelBuilder.Entity<IndividualCheckupIndexValue>(EFConfigure.IndividualCheckupIndexValue);
            modelBuilder.Entity<ContingentCheckupIndexValue>(EFConfigure.ContingentCheckupIndexValue);
            modelBuilder.Entity<CheckupResult>(EFConfigure.CheckupResult);
            modelBuilder.Entity<EmployerDepartment>(EFConfigure.EmployerDepartment);
            modelBuilder.Entity<Gender>(EFConfigure.Gender);
            modelBuilder.Entity<Patient>(EFConfigure.Patient);
            modelBuilder.Entity<PreliminaryMedicalExamination>(EFConfigure.PreliminaryMedicalExamination);
            modelBuilder.Entity<PeriodicMedicalExamination>(EFConfigure.PeriodicMedicalExamination);
            modelBuilder.Entity<IndividualCheckupStatus>(EFConfigure.IndividualCheckupStatus);
            modelBuilder.Entity<ContingentCheckupStatus>(EFConfigure.ContingentCheckupStatus);
            modelBuilder.Entity<NewlyDiagnosedChronicSomaticDisease>(EFConfigure.NewlyDiagnosedChronicSomaticDisease);
            modelBuilder.Entity<NewlyDiagnosedOccupationalDisease>(EFConfigure.NewlyDiagnosedOccupationalDisease);
        }
    }
}
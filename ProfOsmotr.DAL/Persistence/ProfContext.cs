using Microsoft.EntityFrameworkCore;

namespace ProfOsmotr.DAL
{
    public class ProfContext : DbContext
    {
        internal DbSet<Calculation> Calculations { get; private set; }

        internal DbSet<OrderItem> OrderItems { get; private set; }

        internal DbSet<OrderAnnex> OrderAnnex { get; private set; }

        internal DbSet<Profession> Professions { get; private set; }

        internal DbSet<Service> Services { get; private set; }

        internal DbSet<ServiceAvailabilityGroup> ServiceAvailabilityGroups { get; private set; }

        internal DbSet<CalculationResultItem> CalculationResultItems { get; private set; }

        internal DbSet<OrderExamination> OrderExaminations { get; private set; }

        internal DbSet<Clinic> Clinics { get; private set; }

        internal DbSet<User> Users { get; private set; }

        internal DbSet<ClinicRegisterRequest> ClinicRegisterRequests { get; private set; }

        public ProfContext(DbContextOptions<ProfContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActualClinicService>(EFConfigure.ActualClinicService);
            modelBuilder.Entity<OrderExamination>(EFConfigure.OrderExamination);
            modelBuilder.Entity<OrderAnnex>(EFConfigure.OrderAnnex);
            modelBuilder.Entity<OrderItem>(EFConfigure.OrderItem);
            modelBuilder.Entity<OrderItemOrderExamination>(EFConfigure.OrderItemOrderExamination);
            modelBuilder.Entity<ProfessionOrderItem>(EFConfigure.ProfessionOrderItem);
            modelBuilder.Entity<Service>(EFConfigure.Service);
            modelBuilder.Entity<ServiceAvailabilityGroup>(EFConfigure.ServiceAvailabilityGroup);
            modelBuilder.Entity<Calculation>(EFConfigure.Calculation);
            modelBuilder.Entity<User>(EFConfigure.User);
            modelBuilder.Entity<Role>(EFConfigure.Role);
            modelBuilder.Entity<TargetGroup>(EFConfigure.TargetGroup);
            modelBuilder.Entity<ClinicRegisterRequest>(EFConfigure.ClinicRegisterRequest);
        }
    }
}
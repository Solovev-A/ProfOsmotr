using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace ProfOsmotr.DAL
{
    internal static class EFConfigure
    {
        #region Methods

        internal static void ActualClinicService(EntityTypeBuilder<ActualClinicService> builder)
        {
            builder.HasKey(x => new { x.OrderExaminationId, x.ClinicId });
            builder.HasOne(x => x.Service)
                .WithMany()
                .HasForeignKey(x => new { x.ServiceId, x.OrderExaminationId, x.ClinicId });
        }

        internal static void Calculation(EntityTypeBuilder<Calculation> builder)
        {
            builder.HasOne(x => x.Creator)
                .WithMany(x => x.Calculations)
                .HasForeignKey(x => x.CreatorId);
        }

        internal static void ClinicRegisterRequest(EntityTypeBuilder<ClinicRegisterRequest> builder)
        {
            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId);
        }

        internal static void OrderAnnex(EntityTypeBuilder<OrderAnnex> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void OrderExamination(EntityTypeBuilder<OrderExamination> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.TargetGroupId)
                .HasConversion<int>();
            builder.HasOne(x => x.DefaultServiceDetails)
                .WithMany()
                .HasForeignKey(x => x.DefaultServiceDetailsId);
        }

        internal static void OrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.OrderAnnexId)
                .HasConversion<int>();
        }

        internal static void OrderItemOrderExamination(EntityTypeBuilder<OrderItemOrderExamination> builder)
        {
            builder.HasKey(x => new { x.OrderItemId, x.OrderExaminationId });
        }

        internal static void ProfessionOrderItem(EntityTypeBuilder<ProfessionOrderItem> builder)
        {
            builder.HasKey(x => new { x.ProfessionId, x.OrderItemId });
        }

        internal static void Role(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void Service(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => new { x.Id, x.OrderExaminationId, x.ClinicId });
            builder.Property(x => x.ServiceAvailabilityGroupId)
                .HasConversion<int>();
        }

        internal static void ServiceAvailabilityGroup(EntityTypeBuilder<ServiceAvailabilityGroup> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void TargetGroup(EntityTypeBuilder<TargetGroup> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void User(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.RoleId)
                .HasConversion<int>();
            builder.Property(x => x.Username)
                .IsRequired();
            builder.Property(x => x.PasswordHash)
                .IsRequired();
            builder.HasAlternateKey(x => x.Username);
        }

        #endregion Methods
    }
}
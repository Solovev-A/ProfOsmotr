﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProfOsmotr.DAL
{
    internal static class EFConfigure
    {
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

        internal static void CheckupResult(EntityTypeBuilder<CheckupResult> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void ClinicRegisterRequest(EntityTypeBuilder<ClinicRegisterRequest> builder)
        {
            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId);
        }

        internal static void ContingentCheckupIndexValue(EntityTypeBuilder<ContingentCheckupIndexValue> builder)
        {
            builder.HasKey(x => new { x.ContingentCheckupStatusId, x.ExaminationResultIndexId });
        }

        internal static void ContingentCheckupStatus(EntityTypeBuilder<ContingentCheckupStatus> builder)
        {
            builder.HasAlternateKey(x => new { x.PeriodicMedicalExaminationId, x.PatientId });
            builder.Property(x => x.CheckupResultId)
                .HasConversion<int>();
            builder.HasOne(x => x.LastEditor)
                .WithMany()
                .HasForeignKey(x => x.LastEditorId);
        }

        internal static void PeriodicMedicalExamination(EntityTypeBuilder<PeriodicMedicalExamination> builder)
        {
            builder.HasOne(x => x.LastEditor)
                .WithMany()
                .HasForeignKey(x => x.LastEditorId);
        }

        internal static void EmployerDepartment(EntityTypeBuilder<EmployerDepartment> builder)
        {
            builder.HasOne(x => x.Parent)
                .WithMany(e => e.Departments)
                .HasForeignKey(x => x.ParentId);
        }

        internal static void Gender(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion<int>();
        }

        internal static void IndividualCheckupIndexValue(EntityTypeBuilder<IndividualCheckupIndexValue> builder)
        {
            builder.HasKey(x => new { x.IndividualCheckupStatusId, x.ExaminationResultIndexId });
        }

        internal static void IndividualCheckupStatus(EntityTypeBuilder<IndividualCheckupStatus> builder)
        {
            builder.HasOne(x => x.PreliminaryMedicalExamination)
                .WithOne(e => e.CheckupStatus)
                .HasForeignKey<IndividualCheckupStatus>(x => x.PreliminaryMedicalExaminationId);
            builder.HasAlternateKey(x => new { x.PreliminaryMedicalExaminationId, x.PatientId });
            builder.Property(x => x.CheckupResultId)
                .HasConversion<int>();
            builder.HasOne(x => x.LastEditor)
                .WithMany()
                .HasForeignKey(x => x.LastEditorId);
        }

        internal static void PreliminaryMedicalExamination(EntityTypeBuilder<PreliminaryMedicalExamination> builder)
        {
            builder.HasOne(x => x.CheckupStatus)
                .WithOne(c => c.PreliminaryMedicalExamination);
            builder.HasOne(x => x.LastEditor)
                .WithMany()
                .HasForeignKey(x => x.LastEditorId);
        }

        internal static void NewlyDiagnosedChronicSomaticDisease(EntityTypeBuilder<NewlyDiagnosedChronicSomaticDisease> builder)
        {
            builder.HasKey(x => new { x.ContingentCheckupStatusId, x.ICD10ChapterId });
        }

        internal static void NewlyDiagnosedOccupationalDisease(EntityTypeBuilder<NewlyDiagnosedOccupationalDisease> builder)
        {
            builder.HasKey(x => new { x.ContingentCheckupStatusId, x.ICD10ChapterId });
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

        internal static void Patient(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.GenderId)
                .HasConversion<int>();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.PatronymicName).IsRequired();
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
    }
}
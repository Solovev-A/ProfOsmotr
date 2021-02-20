using System;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса работы с хранилищами
    /// </summary>
    public interface IProfUnitOfWork : IDisposable
    {
        ICatalogRepository ActualClinicServices { get; }

        ICalculationRepository Calculations { get; }

        IRepository<CheckupResult> CheckupResults { get; }

        IQueryAwareRepository<ClinicRegisterRequest> ClinicRegisterRequests { get; }

        IQueryAwareRepository<Clinic> Clinics { get; }

        IRepository<ExaminationResultIndex> ExamintaionResultIndexes { get; }

        IRepository<ICD10Chapter> ICD10Chapters { get; }

        IRepository<Gender> Genders { get; }

        IRepository<MedicalExaminationType> MedicalExaminationTypes { get; }

        IRepository<OrderAnnex> OrderAnnexes { get; }

        IOrderExaminationRepository OrderExaminations { get; }

        IOrderRepository OrderItems { get; }

        IRepository<Profession> Professions { get; }

        IRepository<Role> Roles { get; }

        IRepository<ServiceAvailabilityGroup> ServiceAvailabilityGroups { get; }

        IRepository<Service> Services { get; }

        IRepository<TargetGroup> TargetGroups { get; }

        IQueryAwareRepository<User> Users { get; }

        /// <summary>
        /// Сохраняет изменения, сделанные в хранилищах
        /// </summary>
        void Save();

        /// <summary>
        /// Сохраняет изменения, сделанные в хранилищах
        /// </summary>
        Task SaveAsync();
    }
}
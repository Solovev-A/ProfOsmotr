using ProfOsmotr.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для хранилища периодических медосмотров
    /// </summary>
    public interface IPeriodicMedicalExaminationRepository : IQueryAwareRepository<PeriodicMedicalExamination>
    {
        /// <summary>
        /// Производит поиск периодического медосмотра с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор искомого медосмотра</param>
        /// <param name="noTracking"></param>
        /// <returns>null, если поиск не дал результатов</returns>
        Task<PeriodicMedicalExamination> FindExaminationAsync(int id, bool noTracking = false);

        /// <summary>
        /// Производит поиск периодического медосмотра работника с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор искомого периодического медосмотра работника</param>
        /// <param name="noTracking"></param>
        /// <returns>null, если поиск не дал результов</returns>
        Task<ContingentCheckupStatus> FindCheckupStatus(int id, bool noTracking = false);

        /// <summary>
        /// Предоставляет идентифиактор медицинской организации, проводившей периодический медосмотр работника <paramref name="checkupStatusId"/>
        /// </summary>
        /// <param name="checkupStatusId">Идентификатор периодического медосмотра работника</param>
        /// <returns></returns>
        Task<int> GetCheckupStatusClinicIdAsync(int checkupStatusId);

        /// <summary>
        /// Удаляет периодический медосмотр работника <paramref name="checkupStatus"/>
        /// </summary>
        /// <param name="checkupStatus"></param>
        void DeleteCheckupStatus(ContingentCheckupStatus checkupStatus);

        /// <summary>
        /// Подсчитывает количество периодических медосмотров работников по месяцам для
        /// медицинской организации с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<IEnumerable<CountResult>> CountCheckupsByMonth(int clinicId);

        /// <summary>
        /// Предоставляет перечисление  всех медосмотров работников для периодического осмотра с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId"></param>
        /// <returns></returns>
        Task<IEnumerable<ContingentCheckupStatus>> ListAllCheckupStatuses(int examinationId);

        /// <summary>
        /// Производит поиск периодичского медосмотра с идентификатором <paramref name="id"/>, включая данные для заключительного акта
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра</param>
        /// <returns></returns>
        Task<PeriodicMedicalExamination> FindExaminationReportData(int id);

        /// <summary>
        /// Предоставляет перечисление всех периодических медосмотров медицинской организации с идентификатором <paramref name="clinicId"/> за год <paramref name="examinationYear"/>
        /// </summary>
        /// <param name="examinationYear">Год медицинских осмотров</param>
        /// <param name="clinicId">Идентификатор медицинской организации, проводившей осмотры</param>
        /// <returns></returns>
        Task<IEnumerable<PeriodicMedicalExamination>> FindAllExaminations(int examinationYear, int clinicId);
    }
}
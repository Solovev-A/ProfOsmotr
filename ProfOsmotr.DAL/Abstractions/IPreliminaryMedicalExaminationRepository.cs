using ProfOsmotr.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для хранилища предварителных медицинских осмотров
    /// </summary>
    public interface IPreliminaryMedicalExaminationRepository : IQueryAwareRepository<PreliminaryMedicalExamination>
    {
        /// <summary>
        /// Подсчитывает количество проведенных предварительных осмотров для медицинской организации 
        /// с идентификатором <paramref name="clinicId"/> по месяцам
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации, проводившей медосмотры</param>
        /// <returns></returns>
        Task<IEnumerable<CountResult>> CountExaminationsByMonth(int clinicId);

        /// <summary>
        /// Производит поиск предварительного медосмотра с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор предварительного медосмотра</param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        Task<PreliminaryMedicalExamination> FindExaminationAsync(int id, bool noTracking = true);
    }
}
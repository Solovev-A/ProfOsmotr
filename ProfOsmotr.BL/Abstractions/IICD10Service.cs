using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса Международной классификации болезней 10-го пересмотра
    /// </summary>
    public interface IICD10Service
    {
        /// <summary>
        /// Предоставляет список классов болезней, в соответствии с МКБ-10
        /// </summary>
        /// <returns></returns>
        Task<ICD10ChaptersResponse> ListChaptersAsync();

        /// <summary>
        /// Предоставляет класс болезней, в соответствии с МКБ-10, с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор класса болезней</param>
        /// <returns></returns>
        internal Task<ICD10Chapter> FindChapterAsync(int id);
    }
}
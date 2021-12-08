using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса профессий
    /// </summary>
    public interface IProfessionService
    {
        /// <summary>
        /// Создает профессию
        /// </summary>
        /// <param name="request">Исходные данные для создания профессии</param>
        /// <returns></returns>
        Task<ProfessionResponse> CreateProfession(CreateProfessionRequest request);

        /// <summary>
        /// Создает профессию для расчета медицинского осмотра в медицинской организации с
        /// идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="request">Исходные данные для создания профессии</param>
        /// <param name="clinicId">Идентификатор медицинской организации, производящей расчет</param>
        /// <returns></returns>
        Task<ProfessionResponse> CreateProfessionForCalculation(CreateProfessionRequest request, int clinicId);

        /// <summary>
        /// Производит поиск профессии с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор профессии</param>
        /// <returns>null, если поиск не дал результов</returns>
        Task<Profession> FindProfessionAsync(int id);

        /// <summary>
        /// Производит поиск профессий с предположениями
        /// </summary>
        /// <param name="request">Запрос для поиска професий с предположенями</param>
        /// <returns></returns>
        /// <remarks>Предполагаемыми считаются профессии, ранее использованные в медосмотрах определенного работодателя</remarks>
        Task<ProfessionSearchResultResponse> FindProfessionWithSuggestions(FindProfessionRequest request);
    }
}
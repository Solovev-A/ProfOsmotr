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
        Task<ProfessionSearchResultResponse> FindProfessionWithSuggestions(FindProfessionRequest request);
    }
}
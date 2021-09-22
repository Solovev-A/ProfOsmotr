using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IICD10Service
    {
        Task<ICD10ChaptersResponse> ListChaptersAsync();

        internal Task<ICD10Chapter> FindChapterAsync(int id);
    }
}
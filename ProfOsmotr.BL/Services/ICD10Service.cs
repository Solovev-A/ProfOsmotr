using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ICD10Service : IICD10Service
    {
        private IProfUnitOfWork uow;

        public ICD10Service(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<ICD10ChaptersResponse> ListChaptersAsync()
        {
            try
            {
                var result = await uow.ICD10Chapters.GetAllAsync();
                var ordered = result.OrderBy(chapter => chapter.Block);
                return new ICD10ChaptersResponse(ordered);
            }
            catch (Exception ex)
            {
                return new ICD10ChaptersResponse(ex.Message);
            }
        }
    }
}
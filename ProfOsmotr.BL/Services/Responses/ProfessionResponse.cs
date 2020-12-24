using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ сервиса профессий
    /// </summary>
    public class ProfessionResponse : BaseResponse<Profession>
    {
        public ProfessionResponse(Profession entity) : base(entity)
        {
        }

        public ProfessionResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Базовый ответ сервиса приказа в отношении медицинских обследований
    /// </summary>
    public class OrderExaminationResponse : BaseResponse<OrderExamination>
    {
        public OrderExaminationResponse(OrderExamination entity) : base(entity)
        {
        }

        public OrderExaminationResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
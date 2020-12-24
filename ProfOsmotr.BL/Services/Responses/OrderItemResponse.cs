using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ сервиса приказа в отношении элемента приказа
    /// </summary>
    public class OrderItemResponse : BaseResponse<OrderItem>
    {
        /// <summary>
        /// Успешный ответ
        /// </summary>
        /// <param name="entity"></param>
        internal OrderItemResponse(OrderItem entity) : base(entity)
        {
        }

        /// <summary>
        /// Ответ с сообщением об ошибке
        /// </summary>
        /// <param name="errorMessage"></param>
        internal OrderItemResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ на запрос
    /// </summary>
    /// <typeparam name="T">Тип результата</typeparam>
    public abstract class BaseResponse<T>
    {
        /// <summary>
        /// Успешность выполнения запроса
        /// </summary>
        public bool Succeed { get; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Результат обработки запроса
        /// </summary>
        public T Result { get; }

        protected BaseResponse(T entity)
        {
            Succeed = true;
            Message = "OK";
            Result = entity;
        }

        protected BaseResponse(string errorMessage)
        {
            Succeed = false;
            Message = errorMessage;
            Result = default;
        }
    }
}
using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ в отношении результата запроса
    /// </summary>
    /// <typeparam name="T">Тип элементов результата запроса</typeparam>
    public class QueryResponse<T> : BaseResponse<QueryResult<T>>
    {
        public QueryResponse(QueryResult<T> entity) : base(entity)
        {
        }

        public QueryResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ сервиса пользователей
    /// </summary>
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User entity) : base(entity)
        {
        }

        public UserResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
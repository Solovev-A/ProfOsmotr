using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Infrastructure.Validation
{
    public class UsernameAttribute : RegularExpressionAttribute
    {
        public UsernameAttribute()
            : base($@"^[0-9A-Za-z]{{3,20}}$")
        {
            ErrorMessage = "Имя пользователя может содержать только буквы латинского алфавита и цифры. От 3 до 20 символов";
        }
    }
}
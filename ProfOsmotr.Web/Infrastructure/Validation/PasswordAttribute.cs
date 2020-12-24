using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Infrastructure.Validation
{
    public class PasswordAttribute : RegularExpressionAttribute
    {
        public PasswordAttribute()
            : base($@"^[A-Za-z\d!@#$%^&*_+-?]{{8,20}}$")
        {
            ErrorMessage = "Пароль может содержать только буквы латинского алфавита, цифры и спецсимволы. От 8 до 20 символов";
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Infrastructure.Validation
{
    /// <summary>
    /// Допускает буквы латинского алфавита, кириллицы, цифры, дефис и пробел
    /// </summary>
    public class TextValidationAttribute : RegularExpressionAttribute
    {
        public TextValidationAttribute(int minLength, int maxLength)
            : base($@"^[0-9A-Za-zА-Яа-яЁё\s-,\.\(\)]{{{minLength},{maxLength}}}$")
        {
            ErrorMessage = $"Допускаются только буквы, цифры, дефис и пробел. От {minLength} до {maxLength} символов";
        }
    }
}
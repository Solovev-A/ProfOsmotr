using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Infrastructure.Validation
{
    public class CustomPhoneAttribute : RegularExpressionAttribute
    {
        public CustomPhoneAttribute()
            : base($@"^\+7 \d{{3}} \d{{7}}$")
        {
            ErrorMessage = "Телефон должен соответствовать шаблону: +7 123 4567890";
        }
    }
}
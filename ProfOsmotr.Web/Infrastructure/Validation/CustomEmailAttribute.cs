using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Infrastructure.Validation
{
    public class CustomEmailAttribute : RegularExpressionAttribute
    {
        public CustomEmailAttribute()
            : base($@"(?=^.{{1,300}}$)(\S+@\S+\.\S+)")
        {
            ErrorMessage = "Email должен быть похож на xxx@yyyy.zz. До 300 символов";
        }
    }
}
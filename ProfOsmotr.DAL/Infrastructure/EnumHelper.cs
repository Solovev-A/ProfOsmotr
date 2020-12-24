using System;
using System.ComponentModel;
using System.Linq;

namespace ProfOsmotr.DAL
{
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            return value.ToString();
        }
    }
}
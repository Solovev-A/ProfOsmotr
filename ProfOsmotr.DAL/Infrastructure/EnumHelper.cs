using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProfOsmotr.DAL
{
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            var attribute = GetAttribute<DescriptionAttribute>(value);

            return attribute?.Description ?? value.ToString();
        }

        public static string DisplayName(this Enum value)
        {
            var attribute = GetAttribute<DisplayAttribute>(value);

            return attribute?.Name ?? value.ToString();
        }

        private static T GetAttribute<T>(Enum value)
            where T : class
        {
            var attributes = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(T), false);

            return attributes.FirstOrDefault() as T;
        }
    }
}
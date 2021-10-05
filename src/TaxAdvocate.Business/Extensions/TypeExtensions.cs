using System;
using System.ComponentModel;
using System.Linq;

namespace TaxAdvocate.Business.Extensions
{
    public static class TypeExtensions
    {

        public static string GetDescription(this Type source)
        {
            var attribute =
                source.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : string.Empty;
        }

        public static string GetDisplayName(this Type source)
        {
            var attribute =
                source.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
            return attribute != null ? attribute.DisplayName : string.Empty;
        }

    }
}

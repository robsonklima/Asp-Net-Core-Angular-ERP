using System;
using System.ComponentModel;

namespace SAT.MODELS.Extensions
{
    public static class EnumExtension
    {
        public static string Description(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

using SAT.MODELS.Enums;
using System;
using System.ComponentModel;

namespace SAT.API.Support
{
    public static class EnumExtension
    {
        public static string ToDescriptionString(this NomeIndicadorEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

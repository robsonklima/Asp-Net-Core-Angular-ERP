using System.ComponentModel;

namespace SAT.INTEGRACOES.Extensions
{
    public static class EnumExtensions
    {
        public static string StringValue<Enum>(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

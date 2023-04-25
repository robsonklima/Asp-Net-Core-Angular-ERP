using System.Text.RegularExpressions;

namespace SAT.UTILS
{
    public class StringHelper
    {
        public static string GetStringBetweenCharacters(string input, char charFrom, char charTo)
        {
            int posFrom = input.IndexOf(charFrom);
            if (posFrom != -1) //if found char
            {
                int posTo = input.IndexOf(charTo, posFrom + 1);
                if (posTo != -1) //if found char
                {
                    return input.Substring(posFrom + 1, posTo - posFrom - 1);
                }
            }

            return string.Empty;
        }

        public static string RemoverAcentos(string str) {
            return Regex.Replace(str, "[^0-9a-zA-Z]+", "");
        }
    }
}

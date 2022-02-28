using System;

namespace SAT.MODELS.Helpers
{
    public class CriptoHelper
    {
        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.Encoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public static string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.Encoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}

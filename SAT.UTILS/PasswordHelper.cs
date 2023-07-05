using System.Text;
using System.Text.RegularExpressions;

namespace SAT.UTILS
{
    public class PasswordHelper
    {
        // Minimo 8 caracteres, pelo menos uma letra maiuscula, uma letra minuscula, um numero e um caractere especial
        private static readonly string requisitos = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[()[\]{}=\-~,.;<>:@$!%*?&])[A-Za-z\d()[\]{}=\-~,.;<>:@$!%*?&]{8,}$";
        private static readonly string[] caracteresEspeciais = new string[] { "(", ")", "[", "]", "{", "}", "=", "-", "~", ",", ".", ";", "<", ">", ":", "@", "$", "!", "%", "*", "?", "&" };
        public static string GerarNovaSenha(int length = 8)
        {
            StringBuilder builder = new();
            Random random = new();

            for (int i = 0; i < length; i++)
            {
                int tipoCaractere = random.Next(4);
                if (tipoCaractere == 0)
                {
                    builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString().ToLower());
                }
                else if (tipoCaractere == 1)
                {
                    builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString().ToUpper());
                }
                else if (tipoCaractere == 2)
                {
                    builder.Append(new Random().Next(0, 9));
                }
                else
                {
                    builder.Append(caracteresEspeciais[new Random().Next(caracteresEspeciais.Length)]);
                }
            }

            // Valida se o sorteio bate com os requisitos de senha
            bool validaSenha = Regex.Match(builder.ToString(), requisitos).Success;

            // Senha valida
            if (validaSenha)
            {
                return builder.ToString();
            }
            else // Se não gerou uma senha valida, gera novamente
            {
                return GerarNovaSenha(length);
            }
        }
    }
}

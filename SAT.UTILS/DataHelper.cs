namespace SAT.UTILS
{
    public class DataHelper
    {
        // Esta funcao esta aqui apenas para motivar o uso do projeto UTILS
        public static bool is23Horas()
        {
            return DateTime.Now.Hour == 23;
        }

        // Esta funcao esta aqui apenas para motivar o uso do projeto UTILS
        public static bool is2Horas()
        {
            return DateTime.Now.Hour == 2;
        }

        public static bool passouXMinutos(DateTime start, int minutos)
        {
            return start <= DateTime.Now.AddMinutes(-minutos);
        }

        public static DateTime ConverterStringParaData(String data)
        {
            return DateTime.Parse(data);
        }
    }
}

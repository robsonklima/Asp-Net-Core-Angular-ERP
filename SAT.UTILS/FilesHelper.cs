namespace SAT.UTILS {
    public class FilesHelper
    {
        private FilesHelper()
        {}
        
        public static string CheckExtension(string str)
        {
            switch (str.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                case "0M8R4":
                    return "xls";
                case "UESDB":
                    return "xlsx";
                default:
                    return string.Empty;
            }
        }
    }
}
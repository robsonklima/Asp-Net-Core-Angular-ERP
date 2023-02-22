namespace SAT.UTILS
{
    public class StorageBase64
    {
        public static string ConverteStorageEmBase64(string completePath)
        {
            string arquivoPath = completePath; 

            string base64 = string.Empty;
            string extension = string.Empty;

            if (!string.IsNullOrWhiteSpace(arquivoPath))
            {
                extension = Path.GetExtension(arquivoPath);
                byte[] bytes = File.ReadAllBytes(arquivoPath);

                if (bytes.Length > 0)
                {
                    base64 = Convert.ToBase64String(bytes);
                }
            }

            return base64;
        }
    }
}
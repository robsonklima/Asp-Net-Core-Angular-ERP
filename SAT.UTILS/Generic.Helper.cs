using ICSharpCode.SharpZipLib.Zip;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.UTILS
{
    public class GenericHelper
    {
        public static bool IsPOS(OrdemServico os)
        {
            string[] codigos = { "85", "96", "97", "107", "134", "147", "153", "157", "158", "172", "204", "268", "289", "397", "398", "399", "400", "401", "856", "1098", "1121", "1123", "1126", "1146", "1199" };

            return codigos.Contains(os.CodEquip.ToString());
        }

        public static int ObterClientePorChave(string chave)
        {
            if (chave == Constants.INT_ZAF_KEY)
                return (int)Constants.CLIENTE_ZAFFARI;

            throw new Exception("Chave não encontrada");
        }

        public static void CompressDirectory(string DirectoryPath, string OutputFilePath, int CompressionLevel = 9, string pattern="*.pdf")
        {
            try
            {
                string[] filenames = Directory.GetFiles(DirectoryPath, pattern);

                using (ZipOutputStream OutputStream = new ZipOutputStream(File.Create(OutputFilePath)))
                {

                    OutputStream.SetLevel(CompressionLevel);

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        OutputStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            int sourceBytes;

                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                OutputStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }

                    OutputStream.Finish();
                    OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ImportacaoArquivoService : IImportacaoArquivoService   
    {
        public ImportacaoArquivo Criar(ImportacaoArquivo importacaoArquivo)
        {
            string target = Directory.GetCurrentDirectory() + "/Upload/Importacao";

            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            string fileName = importacaoArquivo.NomeAnexo;
            string imgPath = Path.Combine(target, fileName);

            string existsFile = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == fileName.Split('.')[0]);

            if (!string.IsNullOrWhiteSpace(existsFile))
                File.Delete(existsFile);

            byte[] imageBytes = Convert.FromBase64String(importacaoArquivo.Base64);
            File.WriteAllBytes(imgPath, imageBytes);
            
            return importacaoArquivo;
        }
    }
}

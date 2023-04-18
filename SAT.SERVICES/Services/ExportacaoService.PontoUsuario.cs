using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected dynamic GerarTXTPontoUsuario(UsuarioParameters parameters)
		{
            var usuarios = _usuarioRepo.ObterPorParametros(parameters);

            FileStream fs;
            StreamWriter sw;

            string nomeArquivo = "ponto_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt";
            string caminhoArquivo = Path.GetTempPath() + nomeArquivo;

            if (File.Exists(caminhoArquivo))
                File.Delete(caminhoArquivo);

            fs = new FileStream(caminhoArquivo, System.IO.FileMode.Create);
            sw = new StreamWriter(fs);

            foreach (var usuario in usuarios)
            {
                string coletor = usuario?.Filial?.NumColetorPonto.ToString().PadLeft(4, '0');
                string cracha = usuario.NumCracha != null ? usuario.NumCracha.PadLeft(4, '0') : null;

                foreach (var ponto in usuario.PontosUsuario)
                {
                    string data = ponto.DataHoraRegistro.ToString("ddMMyyHHmmss");
                    
                    sw.WriteLine(coletor + data + cracha);
                }
            }

            sw.Close();
            fs.Close();
            
            byte[] file = File.ReadAllBytes(caminhoArquivo);
            return new FileContentResult(file, "application/octet-stream");
        }
    }
}
using System;

namespace SAT.MODELS.Entities
{
    public class IntegracaoCobra
    {
        public int? CodOS { get; set; }
        public string NumOSCliente { get; set; }
        public string NomeTipoArquivoEnviado { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime? DataHoraEnvio { get; set; }
    }
}
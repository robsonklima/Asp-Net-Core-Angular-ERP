using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Integracao
    {
        [Key]
        public int CodIntegracao { get; set; }
        public string NomeIntegracao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string StatusIntegracao { get; set; }
        public DateTime DataUltimaExecucao { get; set; }
        public string MensagemAviso { get; set; }

        [ForeignKey("CodIntegracao")]
        public IntegracaoArquivo IntegracaoArquivo { get; set; }
    }
}

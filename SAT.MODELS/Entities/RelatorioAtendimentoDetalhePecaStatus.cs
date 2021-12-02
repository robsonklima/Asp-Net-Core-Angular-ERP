using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("RATDetalhesPecasStatus")]
    public partial class RelatorioAtendimentoDetalhePecaStatus
    {
        [Key]
        public int CodRATDetalhesPecasStatus { get; set; }
        public int CodRATDetalhesPecas { get; set; }
        public int CodRatPecasStatus { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string Transportadora { get; set; }
        public string NroMinuta { get; set; }
        public DateTime? DataPrevisao { get; set; }
        public DateTime? DataEmbarque { get; set; }
        public DateTime? DataChegada { get; set; }
        public string NroNf { get; set; }
    }
}

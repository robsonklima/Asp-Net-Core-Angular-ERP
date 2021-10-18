using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        [Key]
        public int CodAgendaTecnico { get; set; }
        public int CodTecnico { get; set; }
        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Tipo { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        [ForeignKey("OrdemServico")]
        public int? CodOS { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }
    }
}
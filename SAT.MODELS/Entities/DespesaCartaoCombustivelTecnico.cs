using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DespesaCartaoCombustivelTecnico
    {
        [Key]
        public int CodDespesaCartaoCombustivelTecnico { get; set; }
        public int CodDespesaCartaoCombustivel { get; set; }

        [ForeignKey("CodDespesaCartaoCombustivel")]
        public DespesaCartaoCombustivel DespesaCartaoCombustivel { get; set; }
        [Column(TypeName = "nchar")]
        public int CodTecnico { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}
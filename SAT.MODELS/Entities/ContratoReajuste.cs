using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class ContratoReajuste
    {
        [Key]
        public int CodContratoReajuste { get; set; }
        public int? CodContrato { get; set; }
        public int? CodTipoIndiceReajuste { get; set; }
        
        [ForeignKey("CodTipoIndiceReajuste")]
        public TipoIndiceReajuste TipoIndiceReajuste { get; set; }
        public decimal PercReajuste { get; set; }        
        public byte? IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }    
    }
}

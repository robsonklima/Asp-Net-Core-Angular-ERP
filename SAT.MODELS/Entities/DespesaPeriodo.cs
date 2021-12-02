using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodo
    {
        [Key]
        public int CodDespesaPeriodo { get; set; }
        public int CodDespesaConfiguracao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}

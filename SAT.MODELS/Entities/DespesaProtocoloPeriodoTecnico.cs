using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DespesaProtocoloPeriodoTecnico
    {
        [Key]
        public int CodDespesaProtocolo { get; set; }
        public int CodDespesaPeriodoTecnico { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCredito { get; set; }
        public DateTime? DataHoraCredito { get; set; }
        public string CodUsuarioCreditoCancelado { get; set; }
        public DateTime? DataHoraCreditoCancelado { get; set; }
        public byte? IndCreditado { get; set; }
        public byte? IndAtivo { get; set; }
        public virtual ICollection<DespesaPeriodoTecnico> DespesaPeriodoTecnico { get; set; }
    }
}
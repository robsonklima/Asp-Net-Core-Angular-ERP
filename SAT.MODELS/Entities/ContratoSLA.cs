using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class ContratoSLA
    {
        public int CodContrato { get; set; }
        [ForeignKey("CodContrato")]
        public Contrato Contrato { get; set; }
        public int CodSLA { get; set; }
        [ForeignKey("CodSLA")]
        public AcordoNivelServico SLA { get; set; }
        public byte? IndAgendamento { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}

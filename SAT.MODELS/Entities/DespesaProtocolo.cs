using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaProtocolo
    {
        [Key]
        public int CodDespesaProtocolo { get; set; }
        public int? CodFilial { get; set; }
        public string NomeProtocolo { get; set; }
        public string ObsProtocolo { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndFechamento { get; set; }
        public DateTime? DataHoraFechamento { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? IndIntegracao { get; set; }
        public byte? IndImpresso { get; set; }
    }
}

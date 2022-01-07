using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class MediaAtendimentoTecnico
    {
        [Key]
        public int CodMediaAtendimentoTecnico { get; set; }
        public int CodTecnico { get; set; }
        public int CodTipoIntervencao { get; set; }
        public double MediaEmMinutos { get; set; }
        public DateTime DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
    }
}
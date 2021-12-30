using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class MediaAtendimentoTecnico
    {
        [Key]
        public int CodTecnico { get; set; }
        public int CodTipoIntervencao { get; set; }
        public float MediaEmMinutos { get; set; }
        public DateTime DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
    }
}
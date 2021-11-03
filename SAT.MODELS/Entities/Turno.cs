using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Turno
    {
       [Key]
        public int CodTurno { get; set; }
        public string DescTurno { get; set; }
        public DateTime HoraInicio1 { get; set; }
        public DateTime HoraFim1 { get; set; }
        public DateTime HoraInicio2 { get; set; }
        public DateTime HoraFim2 { get; set; }
        public byte IndAtivo { get; set; }
    }
}

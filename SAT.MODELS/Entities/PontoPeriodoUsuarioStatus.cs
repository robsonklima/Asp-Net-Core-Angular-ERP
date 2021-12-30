using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities {
    public class PontoPeriodoUsuarioStatus
    {
        [Key]
        public int CodPontoPeriodoUsuarioStatus { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoPeriodoIntervaloAcessoData
    {
        [Key]
        public int CodPontoPeriodoIntervaloAcessoData { get; set; }
        public int IntervaloDias { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
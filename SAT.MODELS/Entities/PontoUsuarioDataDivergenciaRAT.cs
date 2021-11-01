using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoUsuarioDataDivergenciaRAT
    {
        [Key]
        public int CodPontoUsuarioDataDivergenciaRat { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodRat { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
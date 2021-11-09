using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoUsuarioDataDivergenciaObservacao
    {
        [Key]
        public int CodPontoUsuarioDataDivergenciaObservacao { get; set; }
        public int CodPontoUsuarioDataDivergencia { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
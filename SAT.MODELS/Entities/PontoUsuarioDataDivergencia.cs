using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataDivergencia
    {
        [Key]
        public int CodPontoUsuarioDataDivergencia { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataModoDivergencia { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public int CodPontoUsuarioDataMotivoDivergencia { get; set; }
        public int? DivergenciaValidada { get; set; }

        [ForeignKey("CodPontoUsuarioDataModoDivergencia")]
        public PontoUsuarioDataModoDivergencia PontoUsuarioDataModoDivergencia { get; set; }

        [ForeignKey("CodPontoUsuarioDataMotivoDivergencia")]
        public PontoUsuarioDataMotivoDivergencia PontoUsuarioDataMotivoDivergencia { get; set; }
    }
}
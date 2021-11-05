using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataValidacao
    {
        [Key]
        public int CodPontoUsuarioDataValidacao { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioDataJustificativaValidacao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
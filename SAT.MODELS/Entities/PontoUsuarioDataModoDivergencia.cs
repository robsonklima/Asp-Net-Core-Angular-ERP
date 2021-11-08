using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataModoDivergencia
    {
        [Key]
        public int CodPontoUsuarioDataModoDivergencia { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
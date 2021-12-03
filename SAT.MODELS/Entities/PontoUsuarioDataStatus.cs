using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataStatus
    {
        [Key]
        public int CodPontoUsuarioDataStatus { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
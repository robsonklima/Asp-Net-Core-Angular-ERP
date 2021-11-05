using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataStatus
    {
        [Key]
        public int CodPontoUsuarioDataStatus { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
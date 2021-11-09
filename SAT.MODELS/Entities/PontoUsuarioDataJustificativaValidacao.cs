using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataJustificativaValidacao
    {
        [Key]
        public int CodPontoUsuarioDataJustificativaValidacao { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataStatusAcesso
    {
        [Key]
        public int CodPontoUsuarioDataStatusAcesso { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
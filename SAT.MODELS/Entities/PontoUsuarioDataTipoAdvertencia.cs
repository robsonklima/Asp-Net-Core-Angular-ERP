using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataTipoAdvertencia
    {
        [Key]
        public int CodPontoUsuarioDataTipoAdvertencia { get; set; }
        public string Descricao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
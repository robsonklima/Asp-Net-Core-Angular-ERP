using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoTipoHora
    {
        [Key]
        public int CodPontoTipoHora { get; set; }
        public string NomeTipoHora { get; set; }
        public byte IndAtivo { get; set; }
    }
}
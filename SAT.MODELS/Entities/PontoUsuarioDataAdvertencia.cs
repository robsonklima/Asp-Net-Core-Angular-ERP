using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoUsuarioDataAdvertencia
    {
        [Key]
        public int CodPontoUsuarioDataAdvertencia { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataTipoAdvertencia { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioAdvertido { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
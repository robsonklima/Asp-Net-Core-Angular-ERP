using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class HistPontoUsuarioDataAdvertencium
    {
        [Key]
        public int CodHistPontoUsuarioDataAdvertencia { get; set; }
        public int? CodPontoUsuarioDataAdvertencia { get; set; }
        public int? CodPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioDataTipoAdvertencia { get; set; }
        [StringLength(20)]
        public string CodUsuarioAdvertido { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(1)]
        public string Operacao { get; set; }
        [StringLength(1)]
        public string TipoTrigger { get; set; }
    }
}

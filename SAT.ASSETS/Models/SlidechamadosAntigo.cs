using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SLIDEChamadosAntigos")]
    public partial class SlidechamadosAntigo
    {
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAbertura { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
    }
}

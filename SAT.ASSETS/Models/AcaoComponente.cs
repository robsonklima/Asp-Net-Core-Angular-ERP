using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("AcaoComponente")]
    public partial class AcaoComponente
    {
        public int CodAcaoComponente { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        public int? CodAcao { get; set; }
        public byte? Selecionado { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
    }
}

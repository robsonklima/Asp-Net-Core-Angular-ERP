using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("InstalacaoInfoBordero")]
    public partial class InstalacaoInfoBordero
    {
        public int CodInstalacao { get; set; }
        public int? IndStatusProtocolado { get; set; }
        public int? CodStatusDocumentocao { get; set; }
        public int? CodStatusInstalacao { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorBordero { get; set; }
        [Required]
        [StringLength(80)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

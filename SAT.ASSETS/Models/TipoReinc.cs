using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TipoReinc")]
    public partial class TipoReinc
    {
        [Column("codTipoReinc")]
        public int CodTipoReinc { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoReinc { get; set; }
    }
}

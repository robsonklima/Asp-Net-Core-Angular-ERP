using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoOR")]
    public partial class TipoOr
    {
        [Key]
        [Column("CodTipoOR")]
        public int CodTipoOr { get; set; }
        [Required]
        [StringLength(50)]
        public string DescricaoTipo { get; set; }
    }
}

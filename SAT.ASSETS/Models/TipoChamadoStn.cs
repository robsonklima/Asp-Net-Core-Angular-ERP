using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoChamadoSTN")]
    public partial class TipoChamadoStn
    {
        [Key]
        [Column("CodTipoChamadoSTN")]
        public int CodTipoChamadoStn { get; set; }
        [Required]
        [Column("DescTipoChamadoSTN")]
        [StringLength(100)]
        public string DescTipoChamadoStn { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}

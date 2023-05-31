using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SLAParametroAdicional")]
    public partial class SlaparametroAdicional
    {
        [Key]
        [Column("CodSLAParametroAdicional")]
        public byte CodSlaparametroAdicional { get; set; }
        [Required]
        [Column("NomeSLAParametroAdicional")]
        [StringLength(100)]
        public string NomeSlaparametroAdicional { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusServicoSTN")]
    public partial class StatusServicoStn
    {
        [Key]
        [Column("CodStatusServicoSTN")]
        public int CodStatusServicoStn { get; set; }
        [Required]
        [Column("DescStatusServicoSTN")]
        [StringLength(50)]
        public string DescStatusServicoStn { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}

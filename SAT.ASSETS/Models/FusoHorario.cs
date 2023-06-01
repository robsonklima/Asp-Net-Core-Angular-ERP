using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FusoHorario")]
    public partial class FusoHorario
    {
        [Key]
        public int CodFusoHorario { get; set; }
        [Required]
        [StringLength(50)]
        public string DescFusoHorario { get; set; }
        [Column("QtdHoraGMT", TypeName = "decimal(10, 2)")]
        public decimal QtdHoraGmt { get; set; }
    }
}

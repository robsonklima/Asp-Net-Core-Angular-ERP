using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ItemDefeito")]
    public partial class ItemDefeito
    {
        [Key]
        public int CodItemDefeito { get; set; }
        [Column("CodORItem")]
        public int CodOritem { get; set; }
        [Required]
        [StringLength(50)]
        public string CodTecnico { get; set; }
        public int CodDefeito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

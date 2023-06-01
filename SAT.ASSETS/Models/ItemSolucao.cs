using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ItemSolucao")]
    public partial class ItemSolucao
    {
        [Key]
        public int CodItemSolucao { get; set; }
        [Column("CodORItem")]
        public int CodOritem { get; set; }
        [Required]
        [StringLength(50)]
        public string CodTecnico { get; set; }
        public int CodSolucao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

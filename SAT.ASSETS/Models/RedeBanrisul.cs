using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RedeBanrisul")]
    public partial class RedeBanrisul
    {
        [Key]
        public int CodRedeBanrisul { get; set; }
        [Required]
        [Column("RedeBanrisul")]
        [StringLength(50)]
        public string RedeBanrisul1 { get; set; }
        public bool Ativo { get; set; }
        public bool? FaturaProduto { get; set; }
        public bool? FaturaServico { get; set; }
    }
}

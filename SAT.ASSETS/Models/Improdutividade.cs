using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Improdutividade")]
    public partial class Improdutividade
    {
        [Key]
        public int CodImprodutividade { get; set; }
        [Required]
        [StringLength(100)]
        public string DescImprodutividade { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LogIntegracaoBB")]
    public partial class LogIntegracaoBb
    {
        [Key]
        [Column("codLogIntegracaoBB")]
        public int CodLogIntegracaoBb { get; set; }
        [Required]
        [StringLength(50)]
        public string Operacao { get; set; }
        [Column("indSucesso")]
        public int IndSucesso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(500)]
        public string NomeArquivo { get; set; }
        [StringLength(5000)]
        public string Descricao { get; set; }
    }
}

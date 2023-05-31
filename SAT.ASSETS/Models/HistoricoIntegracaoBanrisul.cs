using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistoricoIntegracaoBanrisul")]
    public partial class HistoricoIntegracaoBanrisul
    {
        [Key]
        public int CodHistoricoIntegracaoBanrisul { get; set; }
        [Required]
        [StringLength(255)]
        public string NomeArquivo { get; set; }
        public byte IndAbertura { get; set; }
        public byte IndFechamento { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

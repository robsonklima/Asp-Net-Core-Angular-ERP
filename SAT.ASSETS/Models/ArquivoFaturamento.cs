using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ArquivoFaturamento")]
    public partial class ArquivoFaturamento
    {
        [Key]
        public int CodArquivoFaturamento { get; set; }
        public int CodFaturamento { get; set; }
        [Required]
        [StringLength(500)]
        public string Caminho { get; set; }
        [StringLength(100)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int IndAtivo { get; set; }
        [Column("NumNF")]
        [StringLength(30)]
        public string NumNf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEmissao { get; set; }
        [StringLength(60)]
        public string TipoArquivo { get; set; }
        [StringLength(100)]
        public string NomeArquivo { get; set; }
    }
}

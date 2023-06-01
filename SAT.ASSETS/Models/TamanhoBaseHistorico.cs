using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TAMANHO_BASE_HISTORICO")]
    public partial class TamanhoBaseHistorico
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DATA", TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Required]
        [Column("NOME")]
        [StringLength(128)]
        public string Nome { get; set; }
        [Required]
        [Column("TIPO_DADO")]
        [StringLength(128)]
        public string TipoDado { get; set; }
        [Column("TAMANHO_MB")]
        public int TamanhoMb { get; set; }
        [Required]
        [Column("CAMINHO")]
        [StringLength(2000)]
        public string Caminho { get; set; }
    }
}

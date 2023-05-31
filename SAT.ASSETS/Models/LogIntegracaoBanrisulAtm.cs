using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LogIntegracaoBanrisulATM")]
    public partial class LogIntegracaoBanrisulAtm
    {
        [Column("codLogIntegracaoBanrisulATM")]
        public int CodLogIntegracaoBanrisulAtm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroIncidente { get; set; }
        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }
        [Column("indSucesso")]
        public int IndSucesso { get; set; }
        [Column("codOS")]
        public int CodOs { get; set; }
        public int Operacao { get; set; }
    }
}

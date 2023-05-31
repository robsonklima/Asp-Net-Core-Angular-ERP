using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSIntegracao")]
    public partial class Osintegracao
    {
        [Key]
        [Column("CodOSIntegracao")]
        public int CodOsintegracao { get; set; }
        public int CodCliente { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        public int CodStatusServico { get; set; }
        [StringLength(100)]
        public string NomeArquivoGerado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraTabela { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

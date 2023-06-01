using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoBB")]
    public partial class IntegracaoBb
    {
        [Key]
        [Column("CodArquivoIntegracaoBB")]
        public int CodArquivoIntegracaoBb { get; set; }
        [Required]
        [Column("NumOSCliente")]
        [StringLength(30)]
        public string NumOscliente { get; set; }
        public byte IndConfirmacao { get; set; }
        [StringLength(200)]
        public string Motivo { get; set; }
    }
}

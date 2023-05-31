using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSRetornoIntegracao")]
    public partial class PosretornoIntegracao
    {
        public double? TipoRegistro { get; set; }
        [StringLength(255)]
        public string Pessoafiscal { get; set; }
        [Column("CNPJCPF ")]
        public double? Cnpjcpf { get; set; }
        [Column("Rede ")]
        public double? Rede { get; set; }
        [Column("Estabelecimento ")]
        public double? Estabelecimento { get; set; }
        [Column("Terminal ")]
        public double? Terminal { get; set; }
        public double? Serie { get; set; }
        public double? Tipoterminal { get; set; }
        public double? DataInstalacao { get; set; }
        public double? Situacao { get; set; }
        [StringLength(255)]
        public string Obs { get; set; }
        public double? Sequencia { get; set; }
        [StringLength(255)]
        public string F13 { get; set; }
    }
}

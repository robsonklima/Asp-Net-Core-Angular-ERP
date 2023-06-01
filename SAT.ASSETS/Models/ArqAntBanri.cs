using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ArqAntBanri")]
    public partial class ArqAntBanri
    {
        [Column("REDE")]
        public double? Rede { get; set; }
        [Column("TERMINAL")]
        public double? Terminal { get; set; }
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        [Column("RAZAOSOCIAL")]
        [StringLength(255)]
        public string Razaosocial { get; set; }
        [Column("NOMEFANTASIA")]
        [StringLength(255)]
        public string Nomefantasia { get; set; }
        [Column("CNPJORIGEM")]
        public double? Cnpjorigem { get; set; }
        [Column("REDEORIGEM")]
        public double? Redeorigem { get; set; }
        [Column("TERMINALORIGEM")]
        public double? Terminalorigem { get; set; }
        [Column("TIPOTERMINAL")]
        public double? Tipoterminal { get; set; }
    }
}

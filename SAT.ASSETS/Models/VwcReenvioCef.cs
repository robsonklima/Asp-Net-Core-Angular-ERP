using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReenvioCef
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        public int CodStatusServico { get; set; }
        [Column("NumAgenciaNI")]
        [StringLength(10)]
        public string NumAgenciaNi { get; set; }
        [StringLength(100)]
        public string ServicoEmail { get; set; }
    }
}

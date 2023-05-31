using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwFechamentoBanrisul
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(154)]
        public string DataHoraInicio { get; set; }
        [StringLength(154)]
        public string DataHoraSolucao { get; set; }
        public int? QtdMinutos { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        public int CodStatusServico { get; set; }
        public byte? IndFechamentoBanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamentoBanrisul { get; set; }
    }
}

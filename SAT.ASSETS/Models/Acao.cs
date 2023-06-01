using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Acao")]
    public partial class Acao
    {
        [Key]
        public int CodAcao { get; set; }
        [Column("CodEAcao")]
        [StringLength(3)]
        public string CodEacao { get; set; }
        [StringLength(50)]
        public string NomeAcao { get; set; }
        public byte IndPeca { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
        public int? CodStatusServico { get; set; }
    }
}

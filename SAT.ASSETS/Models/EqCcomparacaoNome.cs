using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EqCComparacaoNome")]
    public partial class EqCcomparacaoNome
    {
        [Key]
        [Column("CodEqCComparacaoNome")]
        public int CodEqCcomparacaoNome { get; set; }
        [StringLength(50)]
        public string NomeComparacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndModelo { get; set; }
        [Column("IndSLA")]
        public byte? IndSla { get; set; }
        [StringLength(100)]
        public string CodContrato { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProtocoloChamadoSTN")]
    public partial class ProtocoloChamadoStn
    {
        [Key]
        [Column("CodProtocoloChamadoSTN")]
        public int CodProtocoloChamadoStn { get; set; }
        public int CodAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatahoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column("AcaoSTN")]
        public string AcaoStn { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        [Column("CodTipoChamadoSTN")]
        public int? CodTipoChamadoStn { get; set; }
        [Column("indPrimeiraLigacao")]
        public int? IndPrimeiraLigacao { get; set; }
        [StringLength(200)]
        public string TecnicoCampo { get; set; }
    }
}

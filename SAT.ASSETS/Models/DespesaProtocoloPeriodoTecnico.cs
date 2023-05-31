using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaProtocoloPeriodoTecnico")]
    public partial class DespesaProtocoloPeriodoTecnico
    {
        [Key]
        public int CodDespesaProtocolo { get; set; }
        [Key]
        public int CodDespesaPeriodoTecnico { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioCredito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCredito { get; set; }
        [StringLength(50)]
        public string CodUsuarioCreditoCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCreditoCancelado { get; set; }
        public byte? IndCreditado { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

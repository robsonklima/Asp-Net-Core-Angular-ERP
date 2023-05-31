using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosDesconto")]
    public partial class OrcamentosDesconto
    {
        [Key]
        public int CodOrcamentoDesconto { get; set; }
        public int CodOrcamento { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeCampo { get; set; }
        public int CodTipoDesconto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrDesconto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrOriginal { get; set; }
        [StringLength(500)]
        public string MotivoDesconto { get; set; }

        [ForeignKey(nameof(CodTipoDesconto))]
        [InverseProperty(nameof(TipoDesconto.OrcamentosDescontos))]
        public virtual TipoDesconto CodTipoDescontoNavigation { get; set; }
    }
}

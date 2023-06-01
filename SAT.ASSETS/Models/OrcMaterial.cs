using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcMaterial")]
    public partial class OrcMaterial
    {
        [Key]
        public int CodOrcMaterial { get; set; }
        public int? CodOrc { get; set; }
        [StringLength(50)]
        public string CodigoMagnus { get; set; }
        public int? CodigoPeca { get; set; }
        [StringLength(5000)]
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorUnitario { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorDesconto { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        public int? Quantidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorIpi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorUnitarioFinanceiro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcMaterials))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}

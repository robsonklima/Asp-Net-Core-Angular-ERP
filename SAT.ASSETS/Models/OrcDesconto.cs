using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcDesconto")]
    public partial class OrcDesconto
    {
        [Key]
        public int CodOrcDesconto { get; set; }
        public int? CodOrc { get; set; }
        public int? IndiceCampo { get; set; }
        public int? IndiceTipo { get; set; }
        [StringLength(50)]
        public string NomeCampo { get; set; }
        [StringLength(50)]
        public string NomeTipo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Valor { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        [StringLength(255)]
        public string Motivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcDescontos))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}

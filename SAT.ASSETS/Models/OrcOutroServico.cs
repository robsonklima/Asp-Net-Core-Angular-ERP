using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcOutroServico")]
    public partial class OrcOutroServico
    {
        [Key]
        public int CodOrcOutroServico { get; set; }
        public int? CodOrc { get; set; }
        [StringLength(1000)]
        public string Tipo { get; set; }
        [StringLength(5000)]
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorUnitario { get; set; }
        public int? Quantidade { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcOutroServicos))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}

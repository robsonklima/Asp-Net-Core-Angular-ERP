using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosIss")]
    public partial class OrcamentosIss
    {
        [Key]
        public int CodOrcamentoIss { get; set; }
        public int CodFilial { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrIss { get; set; }

        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.OrcamentosIsses))]
        public virtual Filial CodFilialNavigation { get; set; }
    }
}

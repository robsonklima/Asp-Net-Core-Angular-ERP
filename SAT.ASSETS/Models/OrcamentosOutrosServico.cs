using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class OrcamentosOutrosServico
    {
        [Key]
        public int CodOutrosServicos { get; set; }
        public int CodOrcamento { get; set; }
        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrUnitario { get; set; }
        public int Qtde { get; set; }

        [ForeignKey(nameof(CodOrcamento))]
        [InverseProperty(nameof(Orcamento1.OrcamentosOutrosServicos))]
        public virtual Orcamento1 CodOrcamentoNavigation { get; set; }
    }
}

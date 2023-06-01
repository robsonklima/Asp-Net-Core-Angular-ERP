using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentoNF")]
    public partial class OrcamentoNf
    {
        [Key]
        public int CodOrcamento { get; set; }
        [Key]
        [Column("CodNF")]
        public int CodNf { get; set; }

        [ForeignKey(nameof(CodOrcamento))]
        [InverseProperty(nameof(Orcamento.OrcamentoNfs))]
        public virtual Orcamento CodOrcamentoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosStatus")]
    public partial class OrcamentosStatus
    {
        public OrcamentosStatus()
        {
            Orcamento1s = new HashSet<Orcamento1>();
        }

        [Key]
        public int CodOrcamentoStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatus { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(Orcamento1.CodOrcamentoStatusNavigation))]
        public virtual ICollection<Orcamento1> Orcamento1s { get; set; }
    }
}

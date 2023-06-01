using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosTipo")]
    public partial class OrcamentosTipo
    {
        public OrcamentosTipo()
        {
            Orcamento1s = new HashSet<Orcamento1>();
        }

        [Key]
        public int CodOrcamentoTipo { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipo { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(Orcamento1.CodOrcamentoTipoNavigation))]
        public virtual ICollection<Orcamento1> Orcamento1s { get; set; }
    }
}

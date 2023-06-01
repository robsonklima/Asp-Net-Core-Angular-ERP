using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaAdiantamentoTipo")]
    public partial class DespesaAdiantamentoTipo
    {
        public DespesaAdiantamentoTipo()
        {
            DespesaAdiantamentos = new HashSet<DespesaAdiantamento>();
        }

        [Key]
        public int CodDespesaAdiantamentoTipo { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeAdiantamentoTipo { get; set; }

        [InverseProperty(nameof(DespesaAdiantamento.CodDespesaAdiantamentoTipoNavigation))]
        public virtual ICollection<DespesaAdiantamento> DespesaAdiantamentos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoDesconto")]
    public partial class TipoDesconto
    {
        public TipoDesconto()
        {
            OrcamentosDescontos = new HashSet<OrcamentosDesconto>();
            PedidoPecas = new HashSet<PedidoPeca>();
        }

        [Key]
        public int CodTipoDesconto { get; set; }
        [Required]
        [StringLength(2)]
        public string SiglaTipoDesc { get; set; }
        [Required]
        [StringLength(10)]
        public string NomeTipoDesc { get; set; }

        [InverseProperty(nameof(OrcamentosDesconto.CodTipoDescontoNavigation))]
        public virtual ICollection<OrcamentosDesconto> OrcamentosDescontos { get; set; }
        [InverseProperty(nameof(PedidoPeca.CodTipoDescontoNavigation))]
        public virtual ICollection<PedidoPeca> PedidoPecas { get; set; }
    }
}

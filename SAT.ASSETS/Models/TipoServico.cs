using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoServico")]
    public partial class TipoServico
    {
        public TipoServico()
        {
            ValorServicos = new HashSet<ValorServico>();
        }

        [Key]
        public int CodServico { get; set; }
        [StringLength(50)]
        public string NomeServico { get; set; }
        [Column("CodETipoServico")]
        [StringLength(3)]
        public string CodEtipoServico { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValServico { get; set; }
        public byte? IndValHora { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValPrimHora { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValSegHora { get; set; }
        [Column("indAtivo")]
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(ValorServico.CodServicoNavigation))]
        public virtual ICollection<ValorServico> ValorServicos { get; set; }
    }
}

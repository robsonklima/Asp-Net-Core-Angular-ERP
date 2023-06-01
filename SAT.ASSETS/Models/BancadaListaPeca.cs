using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("BancadaListaPeca")]
    public partial class BancadaListaPeca
    {
        [Key]
        public int CodPeca { get; set; }
        [Key]
        public int CodBancadaLista { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorPeca { get; set; }
        [StringLength(20)]
        public string CodUsuarioAlt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAlt { get; set; }

        [ForeignKey(nameof(CodBancadaLista))]
        [InverseProperty(nameof(BancadaListum.BancadaListaPecas))]
        public virtual BancadaListum CodBancadaListaNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.BancadaListaPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}

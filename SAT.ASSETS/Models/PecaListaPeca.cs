using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecaListaPeca")]
    public partial class PecaListaPeca
    {
        [Key]
        public int CodPecaLista { get; set; }
        [Key]
        public int CodPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaEuro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAtualizacaoValor { get; set; }

        [ForeignKey(nameof(CodPecaLista))]
        [InverseProperty(nameof(PecaListum.PecaListaPecas))]
        public virtual PecaListum CodPecaListaNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PecaListaPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}

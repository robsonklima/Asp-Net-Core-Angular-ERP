using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecaKit")]
    public partial class PecaKit
    {
        [Key]
        public int CodPeca { get; set; }
        [Key]
        public int CodPecaKit { get; set; }
        public int? Qtd { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorUnit { get; set; }

        [ForeignKey(nameof(CodPecaKit))]
        [InverseProperty(nameof(Peca.PecaKitCodPecaKitNavigations))]
        public virtual Peca CodPecaKitNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PecaKitCodPecaNavigations))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}

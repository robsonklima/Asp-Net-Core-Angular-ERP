using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContratoPeca")]
    public partial class ContratoPeca
    {
        [Key]
        public int CodContrato { get; set; }
        [Key]
        public int CodPeca { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValPeca { get; set; }
        [Column("ValIPI")]
        public byte? ValIpi { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ContratoPecas))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.ContratoPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}

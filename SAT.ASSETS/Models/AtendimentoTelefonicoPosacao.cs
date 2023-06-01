using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AtendimentoTelefonicoPOSAcao")]
    public partial class AtendimentoTelefonicoPosacao
    {
        [Key]
        [Column("CodAtendimentoTelefonicoPOSAcao")]
        public int CodAtendimentoTelefonicoPosacao { get; set; }
        [Column("CodAtendimentoTelefonicoPOS")]
        public int CodAtendimentoTelefonicoPos { get; set; }
        [Column("CodAcaoPOS")]
        public int CodAcaoPos { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey(nameof(CodAcaoPos))]
        [InverseProperty(nameof(AcaoPo.AtendimentoTelefonicoPosacaos))]
        public virtual AcaoPo CodAcaoPosNavigation { get; set; }
        [ForeignKey(nameof(CodAtendimentoTelefonicoPos))]
        [InverseProperty(nameof(AtendimentoTelefonicoPo.AtendimentoTelefonicoPosacaos))]
        public virtual AtendimentoTelefonicoPo CodAtendimentoTelefonicoPosNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SLASicrediPOS")]
    public partial class SlasicrediPo
    {
        [Key]
        [Column("CodSLASicredi")]
        public int CodSlasicredi { get; set; }
        public int CodCidade { get; set; }
        public int HorasAtendimentoCorretiva { get; set; }
        public int DiasAtendimentoInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.SlasicrediPos))]
        public virtual Cidade CodCidadeNavigation { get; set; }
    }
}

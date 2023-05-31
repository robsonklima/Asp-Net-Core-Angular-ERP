using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChamadoOS")]
    public partial class ChamadoO
    {
        [Key]
        [Column("CodChamadoOS")]
        public int CodChamadoOs { get; set; }
        public int CodChamado { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodTipoOsGeracao { get; set; }

        [ForeignKey(nameof(CodChamado))]
        [InverseProperty(nameof(Chamado.ChamadoOs))]
        public virtual Chamado CodChamadoNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.ChamadoO))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodTipoOsGeracao))]
        [InverseProperty(nameof(TipoOsgeracao.ChamadoOs))]
        public virtual TipoOsgeracao CodTipoOsGeracaoNavigation { get; set; }
    }
}

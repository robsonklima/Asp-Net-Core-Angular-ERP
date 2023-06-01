using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CidadePOSDePara")]
    public partial class CidadePosdePara
    {
        [Key]
        [Column("CodCidadePOSDePara")]
        public int CodCidadePosdePara { get; set; }
        [Column("CodCidadePOS")]
        public int CodCidadePos { get; set; }
        public int CodCidade { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.CidadePosdeParas))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodCidadePos))]
        [InverseProperty(nameof(CidadePo.CidadePosdeParas))]
        public virtual CidadePo CodCidadePosNavigation { get; set; }
    }
}

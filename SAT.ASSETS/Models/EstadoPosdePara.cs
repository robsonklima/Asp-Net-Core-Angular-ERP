using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EstadoPOSDePara")]
    public partial class EstadoPosdePara
    {
        [Key]
        [Column("CodEstadoPOSDePara")]
        public int CodEstadoPosdePara { get; set; }
        [Column("CodEstadoPOS")]
        public int CodEstadoPos { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }

        [ForeignKey(nameof(CodEstadoPos))]
        [InverseProperty(nameof(EstadoPo.EstadoPosdeParas))]
        public virtual EstadoPo CodEstadoPosNavigation { get; set; }
        [ForeignKey(nameof(CodUf))]
        [InverseProperty(nameof(Uf.EstadoPosdeParas))]
        public virtual Uf CodUfNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EstadoPOS")]
    public partial class EstadoPo
    {
        public EstadoPo()
        {
            CidadePos = new HashSet<CidadePo>();
            EstadoPosdeParas = new HashSet<EstadoPosdePara>();
            RegiaoEstadoPos = new HashSet<RegiaoEstadoPo>();
        }

        [Key]
        public int CodEstado { get; set; }
        [Required]
        [StringLength(200)]
        public string NomeEstado { get; set; }
        [Required]
        [StringLength(2)]
        public string SiglaEstado { get; set; }
        [Required]
        [Column("CodIBGE")]
        [StringLength(30)]
        public string CodIbge { get; set; }
        public int? CodCidadeCapital { get; set; }

        [ForeignKey(nameof(CodCidadeCapital))]
        [InverseProperty(nameof(CidadePo.EstadoPos))]
        public virtual CidadePo CodCidadeCapitalNavigation { get; set; }
        [InverseProperty(nameof(CidadePo.CodEstadoNavigation))]
        public virtual ICollection<CidadePo> CidadePos { get; set; }
        [InverseProperty(nameof(EstadoPosdePara.CodEstadoPosNavigation))]
        public virtual ICollection<EstadoPosdePara> EstadoPosdeParas { get; set; }
        [InverseProperty(nameof(RegiaoEstadoPo.CodEstadoPosNavigation))]
        public virtual ICollection<RegiaoEstadoPo> RegiaoEstadoPos { get; set; }
    }
}

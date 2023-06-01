using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CidadePOS")]
    public partial class CidadePo
    {
        public CidadePo()
        {
            CidadePosdeParas = new HashSet<CidadePosdePara>();
            EstadoPos = new HashSet<EstadoPo>();
        }

        [Key]
        public int CodCidade { get; set; }
        [Required]
        [StringLength(500)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("CodIBGE")]
        [StringLength(30)]
        public string CodIbge { get; set; }
        public int CodEstado { get; set; }
        public int? Populacao { get; set; }
        public int? DistanciaCapital { get; set; }
        public int? TempoCapital { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }

        [ForeignKey(nameof(CodEstado))]
        [InverseProperty(nameof(EstadoPo.CidadePos))]
        public virtual EstadoPo CodEstadoNavigation { get; set; }
        [InverseProperty(nameof(CidadePosdePara.CodCidadePosNavigation))]
        public virtual ICollection<CidadePosdePara> CidadePosdeParas { get; set; }
        [InverseProperty(nameof(EstadoPo.CodCidadeCapitalNavigation))]
        public virtual ICollection<EstadoPo> EstadoPos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoPOSHist")]
    public partial class EquipamentoPoshist
    {
        [Key]
        [Column("CodEquipamentoPOSHist")]
        public int CodEquipamentoPoshist { get; set; }
        [Column("CodEquipamentoPOS")]
        public int CodEquipamentoPos { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Column("CodTipoHistoricoEquipamentoPOS")]
        public int CodTipoHistoricoEquipamentoPos { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("OpPQM")]
        public int? OpPqm { get; set; }
        [Column("CodStatusEquipamentoPOS")]
        public int CodStatusEquipamentoPos { get; set; }
        [Column("CodStatusEquipamentoPOSAnterior")]
        public int? CodStatusEquipamentoPosanterior { get; set; }

        [ForeignKey(nameof(CodEquipamentoPos))]
        [InverseProperty(nameof(EquipamentoPo.EquipamentoPoshists))]
        public virtual EquipamentoPo CodEquipamentoPosNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.EquipamentoPoshists))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodStatusEquipamentoPos))]
        [InverseProperty(nameof(StatusEquipamentoPo.EquipamentoPoshistCodStatusEquipamentoPosNavigations))]
        public virtual StatusEquipamentoPo CodStatusEquipamentoPosNavigation { get; set; }
        [ForeignKey(nameof(CodStatusEquipamentoPosanterior))]
        [InverseProperty(nameof(StatusEquipamentoPo.EquipamentoPoshistCodStatusEquipamentoPosanteriorNavigations))]
        public virtual StatusEquipamentoPo CodStatusEquipamentoPosanteriorNavigation { get; set; }
        [ForeignKey(nameof(CodTipoHistoricoEquipamentoPos))]
        [InverseProperty(nameof(TipoHistoricoEquipamentoPo.EquipamentoPoshists))]
        public virtual TipoHistoricoEquipamentoPo CodTipoHistoricoEquipamentoPosNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.EquipamentoPoshists))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}

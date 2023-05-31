using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoPOS")]
    public partial class EquipamentoPo
    {
        public EquipamentoPo()
        {
            EquipamentoPoshists = new HashSet<EquipamentoPoshist>();
        }

        [Key]
        [Column("CodEquipamentoPOS")]
        public int CodEquipamentoPos { get; set; }
        [Required]
        [StringLength(100)]
        public string NumeroSerie { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataProducao { get; set; }
        public int OpPqm { get; set; }
        public int IdOpSerie { get; set; }
        [Column("CodStatusEquipamentoPOS")]
        public int CodStatusEquipamentoPos { get; set; }
        public int CodTipoMidia { get; set; }
        [Required]
        [Column("CodEquipPQM")]
        [StringLength(50)]
        public string CodEquipPqm { get; set; }
        [Required]
        [Column("NumeroSeriePQM")]
        [StringLength(50)]
        public string NumeroSeriePqm { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLogico { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoPos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodStatusEquipamentoPos))]
        [InverseProperty(nameof(StatusEquipamentoPo.EquipamentoPos))]
        public virtual StatusEquipamentoPo CodStatusEquipamentoPosNavigation { get; set; }
        [ForeignKey(nameof(CodTipoMidia))]
        [InverseProperty(nameof(TipoMidiaPo.EquipamentoPos))]
        public virtual TipoMidiaPo CodTipoMidiaNavigation { get; set; }
        [InverseProperty(nameof(EquipamentoPoshist.CodEquipamentoPosNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshists { get; set; }
    }
}

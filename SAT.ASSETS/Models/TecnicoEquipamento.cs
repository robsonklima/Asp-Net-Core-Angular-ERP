using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicoEquipamento")]
    public partial class TecnicoEquipamento
    {
        [Key]
        public int CodTechEquip { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodFilial { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodEquipContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFim { get; set; }
        [Column("CodEAcao")]
        public int? CodEacao { get; set; }
        public int? IndAtivo { get; set; }

        [ForeignKey(nameof(CodEquipContrato))]
        [InverseProperty(nameof(EquipamentoContrato.TecnicoEquipamentos))]
        public virtual EquipamentoContrato CodEquipContratoNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.TecnicoEquipamentos))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.TecnicoEquipamentos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ContratoEquipMultum
    {
        [Key]
        public int CodContratoEquipMulta { get; set; }
        [Column("DDia")]
        public int Ddia { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("ADia")]
        public int? Adia { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int CodTipoMulta { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercMulta { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public int? CodTipoPercMulta { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrMulta { get; set; }

        [ForeignKey("CodContrato,CodTipoEquip,CodGrupoEquip,CodEquip")]
        [InverseProperty(nameof(ContratoEquipamento.ContratoEquipMulta))]
        public virtual ContratoEquipamento Cod { get; set; }
        [ForeignKey(nameof(CodTipoMulta))]
        [InverseProperty(nameof(TipoMultum.ContratoEquipMulta))]
        public virtual TipoMultum CodTipoMultaNavigation { get; set; }
        [ForeignKey(nameof(CodTipoPercMulta))]
        [InverseProperty(nameof(TipoPercMultum.ContratoEquipMulta))]
        public virtual TipoPercMultum CodTipoPercMultaNavigation { get; set; }
    }
}

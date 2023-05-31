using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TipoMultum
    {
        public TipoMultum()
        {
            ContratoEquipMulta = new HashSet<ContratoEquipMultum>();
        }

        [Key]
        public int CodTipoMulta { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeTipoMulta { get; set; }
        [StringLength(50)]
        public string DescTipoMulta { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoEquipMultum.CodTipoMultaNavigation))]
        public virtual ICollection<ContratoEquipMultum> ContratoEquipMulta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TipoPercMultum
    {
        public TipoPercMultum()
        {
            ContratoEquipMulta = new HashSet<ContratoEquipMultum>();
        }

        [Key]
        public int CodTipoPercMulta { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipoPercMulta { get; set; }
        [StringLength(50)]
        public string DescTipoPercMulta { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoEquipMultum.CodTipoPercMultaNavigation))]
        public virtual ICollection<ContratoEquipMultum> ContratoEquipMulta { get; set; }
    }
}

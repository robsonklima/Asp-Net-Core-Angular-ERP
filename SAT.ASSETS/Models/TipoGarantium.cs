using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TipoGarantium
    {
        public TipoGarantium()
        {
            ContratoEquipamentos = new HashSet<ContratoEquipamento>();
        }

        [Key]
        public int CodTipoGarantia { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaTipoGarantia { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeTipoGarantia { get; set; }
        [StringLength(50)]
        public string DescTipoGarantia { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoEquipamento.CodTipoGarantiaNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentos { get; set; }
    }
}

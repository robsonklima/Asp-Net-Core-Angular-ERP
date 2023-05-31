using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ContratoEquipDatum
    {
        public ContratoEquipDatum()
        {
            ContratoEquipamentoCodContratoEquipDataEntNavigations = new HashSet<ContratoEquipamento>();
            ContratoEquipamentoCodContratoEquipDataGarNavigations = new HashSet<ContratoEquipamento>();
            ContratoEquipamentoCodContratoEquipDataInsNavigations = new HashSet<ContratoEquipamento>();
        }

        [Key]
        public int CodContratoEquipData { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeData { get; set; }
        [StringLength(50)]
        public string DescData { get; set; }
        public byte IndEntrega { get; set; }
        public byte IndInstalacao { get; set; }
        public byte IndGarantia { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoEquipamento.CodContratoEquipDataEntNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentoCodContratoEquipDataEntNavigations { get; set; }
        [InverseProperty(nameof(ContratoEquipamento.CodContratoEquipDataGarNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentoCodContratoEquipDataGarNavigations { get; set; }
        [InverseProperty(nameof(ContratoEquipamento.CodContratoEquipDataInsNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentoCodContratoEquipDataInsNavigations { get; set; }
    }
}

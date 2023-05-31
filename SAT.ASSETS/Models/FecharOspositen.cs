using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FecharOSPOSItens")]
    public partial class FecharOspositen
    {
        [Key]
        [Column("CodFecharOSPOSItens")]
        public int CodFecharOspositens { get; set; }
        [Column("CodFecharOSPOS")]
        public int CodFecharOspos { get; set; }
        public int CodEquipamento { get; set; }
        public bool Instalado { get; set; }
        [Required]
        [StringLength(50)]
        public string Serie { get; set; }
        public int CodOperadoraTelefonia { get; set; }

        [ForeignKey(nameof(CodFecharOspos))]
        [InverseProperty(nameof(FecharOspo.FecharOspositens))]
        public virtual FecharOspo CodFecharOsposNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefonia))]
        [InverseProperty(nameof(OperadoraTelefonium.FecharOspositens))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaNavigation { get; set; }
    }
}

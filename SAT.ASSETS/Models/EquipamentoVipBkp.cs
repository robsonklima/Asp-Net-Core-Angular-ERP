using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoVip_bkp")]
    public partial class EquipamentoVipBkp
    {
        [Key]
        public int CodEquipamentoVip { get; set; }
        public int CodEquipContrato { get; set; }
        [Required]
        [StringLength(5000)]
        public string Emails { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodEquipContrato))]
        [InverseProperty(nameof(EquipamentoContrato.EquipamentoVipBkps))]
        public virtual EquipamentoContrato CodEquipContratoNavigation { get; set; }
    }
}

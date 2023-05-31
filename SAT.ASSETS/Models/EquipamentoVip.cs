using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("EquipamentoVip")]
    public partial class EquipamentoVip
    {
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
    }
}

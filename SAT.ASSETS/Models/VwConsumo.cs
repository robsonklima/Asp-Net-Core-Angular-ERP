using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwConsumo
    {
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(24)]
        public string CodMagnus { get; set; }
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCusto { get; set; }
        public int? QtdPeca { get; set; }
        public int? TotalEquipFilial { get; set; }
        public int? TotalEquip { get; set; }
    }
}

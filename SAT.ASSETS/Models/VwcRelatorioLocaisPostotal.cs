using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelatorioLocaisPostotal
    {
        [Required]
        [StringLength(23)]
        public string TotaldeEquipamentos { get; set; }
        public int? Quantidade { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcKitPadrao
    {
        [Required]
        [Column("KIT Padrão")]
        [StringLength(54)]
        public string KitPadrão { get; set; }
        [Required]
        [StringLength(24)]
        public string Magnus { get; set; }
        [Required]
        [Column("Descrição Peça")]
        [StringLength(80)]
        public string DescriçãoPeça { get; set; }
    }
}

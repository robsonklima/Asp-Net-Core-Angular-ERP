using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDefeitoPeca
    {
        [Column("CodRATDetalhe")]
        public int CodRatdetalhe { get; set; }
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
    }
}

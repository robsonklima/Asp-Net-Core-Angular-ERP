using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class FilialAnalistum
    {
        [Column("codFilialAnalista")]
        public int CodFilialAnalista { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
    }
}

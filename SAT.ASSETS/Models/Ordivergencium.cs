using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORDIVERGENCIA")]
    public partial class Ordivergencium
    {
        [Key]
        public int CodDivergencia { get; set; }
        [Required]
        [StringLength(120)]
        public string DivergenciaDescricao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FrotaFinalidadeUso")]
    public partial class FrotaFinalidadeUso
    {
        public int CodFrotaFinalidadeUso { get; set; }
        [Required]
        [StringLength(50)]
        public string DescFrotaFinalidadeUso { get; set; }
    }
}

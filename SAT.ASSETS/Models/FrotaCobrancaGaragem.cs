using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FrotaCobrancaGaragem")]
    public partial class FrotaCobrancaGaragem
    {
        public int CodFrotaCobrancaGaragem { get; set; }
        [Required]
        [StringLength(50)]
        public string DescFrotaCobrancaGaragem { get; set; }
    }
}

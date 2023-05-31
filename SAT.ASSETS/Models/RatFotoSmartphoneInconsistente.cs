using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RatFotoSmartphoneInconsistente")]
    public partial class RatFotoSmartphoneInconsistente
    {
        [Required]
        [StringLength(220)]
        public string NomeFoto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

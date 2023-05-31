using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SatMobileAppVersao")]
    public partial class SatMobileAppVersao
    {
        [StringLength(20)]
        public string AppVersao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
    }
}

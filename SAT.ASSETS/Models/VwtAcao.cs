using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtAcao
    {
        public int CodAcao { get; set; }
        [Column("CodEAcao")]
        [StringLength(3)]
        public string CodEacao { get; set; }
        [StringLength(50)]
        public string NomeAcao { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

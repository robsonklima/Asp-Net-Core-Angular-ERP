using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SatMobileAppVersaoLog")]
    public partial class SatMobileAppVersaoLog
    {
        [Key]
        public int CodSatMobileAppVersaoLog { get; set; }
        [StringLength(10)]
        public string Versao { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

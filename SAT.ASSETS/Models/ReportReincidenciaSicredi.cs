using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ReportReincidenciaSicredi")]
    public partial class ReportReincidenciaSicredi
    {
        public int? Dia { get; set; }
        [Column("OS")]
        public int? Os { get; set; }
        public int? Equipamentos { get; set; }
        [Column("MaisOS")]
        public int? MaisOs { get; set; }
        [Column("OSSemReinc")]
        public int? OssemReinc { get; set; }
        [StringLength(10)]
        public string Reincidencia { get; set; }
    }
}

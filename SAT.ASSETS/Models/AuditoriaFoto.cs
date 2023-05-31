using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AuditoriaFoto")]
    public partial class AuditoriaFoto
    {
        [Key]
        public int CodAuditoriaFoto { get; set; }
        public int? CodAuditoria { get; set; }
        [StringLength(100)]
        public string Foto { get; set; }
    }
}

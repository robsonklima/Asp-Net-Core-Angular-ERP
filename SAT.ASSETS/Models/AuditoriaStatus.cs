using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AuditoriaStatus")]
    public partial class AuditoriaStatus
    {
        [Key]
        public int CodAuditoriaStatus { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
    }
}

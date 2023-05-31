using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusInstalacao")]
    public partial class StatusInstalacao
    {
        [Key]
        public int CodStatusInstalacao { get; set; }
        [StringLength(500)]
        public string NomeStatusInstalacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalStatus")]
    public partial class InstalStatus
    {
        [Key]
        public int CodInstalStatus { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeInstalStatus { get; set; }
        public byte IndAtivo { get; set; }
    }
}

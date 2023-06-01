using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AutorizadaEmailSicredi")]
    public partial class AutorizadaEmailSicredi
    {
        [Key]
        public int CodEmail { get; set; }
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

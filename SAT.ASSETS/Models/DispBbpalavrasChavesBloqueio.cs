using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBPalavrasChavesBloqueio")]
    public partial class DispBbpalavrasChavesBloqueio
    {
        [Column("CodDispBBPalavrasChavesBloqueio")]
        public int CodDispBbpalavrasChavesBloqueio { get; set; }
        [Required]
        [StringLength(50)]
        public string Palavra { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}

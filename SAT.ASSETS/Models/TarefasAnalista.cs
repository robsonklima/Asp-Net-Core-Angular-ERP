using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class TarefasAnalista
    {
        [Column("codAnalista")]
        public int CodAnalista { get; set; }
        [Required]
        [Column("nomeAnalista")]
        [StringLength(50)]
        public string NomeAnalista { get; set; }
        [Column("indAtivo")]
        public int IndAtivo { get; set; }
    }
}

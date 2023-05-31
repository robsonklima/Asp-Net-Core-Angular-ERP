using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Severidade")]
    public partial class Severidade
    {
        public int CodSeveridade { get; set; }
        [Required]
        [StringLength(50)]
        public string Nivel { get; set; }
        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(500)]
        public string Exemplos { get; set; }
        public int? CodContrato { get; set; }
    }
}

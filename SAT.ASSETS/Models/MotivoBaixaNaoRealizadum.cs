using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class MotivoBaixaNaoRealizadum
    {
        [Key]
        public int CodMotivoBaixaNaoRealizada { get; set; }
        [Required]
        [StringLength(200)]
        public string MotivoBaixaNaoRealizada { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }
    }
}

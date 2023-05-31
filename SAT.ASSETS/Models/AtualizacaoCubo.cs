using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AtualizacaoCubo")]
    public partial class AtualizacaoCubo
    {
        [Key]
        public int CodAtualizacaoCubo { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCuboAtualizado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraAtualizacao { get; set; }
    }
}

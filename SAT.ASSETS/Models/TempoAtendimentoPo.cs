using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TempoAtendimentoPOS")]
    public partial class TempoAtendimentoPo
    {
        [Key]
        [Column("CodTempoAtendimentoPOS")]
        public int CodTempoAtendimentoPos { get; set; }
        [Required]
        [Column("NomeTempoAtendimentoPOS")]
        [StringLength(100)]
        public string NomeTempoAtendimentoPos { get; set; }
        public int TempoInicio { get; set; }
        public int TempoFim { get; set; }
        [Required]
        [StringLength(50)]
        public string Cor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AvaliacaoAtendimento")]
    public partial class AvaliacaoAtendimento
    {
        [Key]
        public int CodAvaliacaoAtendimento { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        public int? Estrelas { get; set; }
        [StringLength(500)]
        public string Observacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public byte? IndEmailEnviado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvio { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MensagemAutomatica")]
    public partial class MensagemAutomatica
    {
        [Key]
        public int CodMensagemAutomatica { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioDestinatario { get; set; }
        public double? Pendencia { get; set; }
        public double? Reincidencia { get; set; }
        [Column("SLA")]
        public double? Sla { get; set; }
        public double? MediaAtendimentosDia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

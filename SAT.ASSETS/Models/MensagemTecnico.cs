using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MensagemTecnico")]
    public partial class MensagemTecnico
    {
        [Key]
        public int CodMensagemTecnico { get; set; }
        [Required]
        [StringLength(1000)]
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioDestinatario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public byte? IndLeitura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraLeitura { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

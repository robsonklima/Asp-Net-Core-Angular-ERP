using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoEnvioEmail")]
    public partial class IntegracaoEnvioEmail
    {
        [Key]
        public int CodIntegracaoEnvioEmail { get; set; }
        [Required]
        [StringLength(500)]
        public string Remetente { get; set; }
        [Required]
        [StringLength(500)]
        public string Destinatario { get; set; }
        [Required]
        [StringLength(1000)]
        public string Assunto { get; set; }
        [Required]
        public string CorpoMensagem { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraEnvio { get; set; }
    }
}

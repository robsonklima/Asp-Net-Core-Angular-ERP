using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class EnvioEmailReincidencium
    {
        [Key]
        [Column("codEnvioEmailReincidencia")]
        public int CodEnvioEmailReincidencia { get; set; }
        [Column("codLogServico")]
        public int CodLogServico { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(1000)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraEnvio { get; set; }
        public int? Status { get; set; }
        [StringLength(5000)]
        public string Observacao { get; set; }
    }
}

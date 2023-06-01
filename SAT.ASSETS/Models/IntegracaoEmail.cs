using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoEmail")]
    public partial class IntegracaoEmail
    {
        [Key]
        public int CodIntegracaoEmail { get; set; }
        [Required]
        public string Conteudo { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Cliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        public byte IndIntegrado { get; set; }
        public byte IndAtivo { get; set; }
    }
}

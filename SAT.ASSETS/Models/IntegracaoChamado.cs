using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoChamado")]
    public partial class IntegracaoChamado
    {
        [Key]
        public int CodIntegracaoChamado { get; set; }
        public int CodIntegracaoEmail { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
    }
}

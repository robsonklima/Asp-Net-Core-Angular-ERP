using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRatbrb
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFechamento { get; set; }
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InicioAtendimento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcListaRatsBloqueio
    {
        [StringLength(281)]
        public string Dados { get; set; }
        [Required]
        [StringLength(524)]
        public string RelatoSolucao { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
    }
}

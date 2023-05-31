using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcListaOssBloqueio
    {
        [StringLength(386)]
        public string Dados { get; set; }
        [StringLength(3527)]
        public string DefeitoRelatado { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int IndBloqueioReincidencia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
    }
}

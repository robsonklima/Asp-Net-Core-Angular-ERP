using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcListaOssHistorico
    {
        [StringLength(320)]
        public string Dados { get; set; }
        [StringLength(3527)]
        public string DefeitoRelatado { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int IndBloqueioReincidencia { get; set; }
    }
}

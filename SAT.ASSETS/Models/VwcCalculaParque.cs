using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcCalculaParque
    {
        [StringLength(20)]
        public string Pai { get; set; }
        public int Filial { get; set; }
        [Column("QTD")]
        public int? Qtd { get; set; }
    }
}

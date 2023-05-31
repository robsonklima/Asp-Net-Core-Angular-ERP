using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("PlanejamentoFuturo")]
    public partial class PlanejamentoFuturo
    {
        public int CodPlanejamento { get; set; }
        [StringLength(50)]
        public string CodPai { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        public int? Quantidade { get; set; }
    }
}

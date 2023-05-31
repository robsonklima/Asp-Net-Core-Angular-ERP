using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class TabelaFiliai
    {
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? QtdColaboradores { get; set; }
        public int? QtdTotalColaboradores { get; set; }
    }
}

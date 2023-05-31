using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class FilialTecnico
    {
        public int CodTecnicosFilial { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(20)]
        public string Descricao { get; set; }
        public int? Qtd { get; set; }
        public int? Ordem { get; set; }
    }
}

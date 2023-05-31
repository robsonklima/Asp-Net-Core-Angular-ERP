using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTecnicosDisponibilidade
    {
        public int? TecnicosEmFerias { get; set; }
        public int? TecnicosSemChamados { get; set; }
        public int? TecnicosChamadosCorretivos { get; set; }
        [Column("TecnicosCChamadosNaoCorretivos")]
        public int? TecnicosCchamadosNaoCorretivos { get; set; }
        public int? TotaldeTecnicos { get; set; }
    }
}

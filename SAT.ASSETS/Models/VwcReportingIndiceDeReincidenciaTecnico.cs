using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDeReincidenciaTecnico
    {
        public int? CodTecnico { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesReinc { get; set; }
    }
}

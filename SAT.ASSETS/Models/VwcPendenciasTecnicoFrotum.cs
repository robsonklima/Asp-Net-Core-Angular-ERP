using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPendenciasTecnicoFrotum
    {
        public int CodTecnico { get; set; }
        public int? Pendencias { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwCodLogixContratoAtivo
    {
        [Column("CODLOGIX")]
        [StringLength(15)]
        public string Codlogix { get; set; }
        [StringLength(2000)]
        public string ObjetoContrato { get; set; }
    }
}

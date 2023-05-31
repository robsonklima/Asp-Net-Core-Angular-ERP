using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcContratoLogistica
    {
        public int CodContrato { get; set; }
        [StringLength(31)]
        public string NomeContrato { get; set; }
        public int? CodCliente { get; set; }
    }
}

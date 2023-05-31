using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ReportingImagen
    {
        public int? CodCliente { get; set; }
        [StringLength(200)]
        public string LocalImagem { get; set; }
    }
}

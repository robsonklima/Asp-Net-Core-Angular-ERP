using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicoCliente")]
    public partial class TecnicoCliente
    {
        [Key]
        public int CodTecnicoCliente { get; set; }
        public int CodTecnico { get; set; }
        public int CodCliente { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ControleConsolidado")]
    public partial class ControleConsolidado
    {
        public int CodControleConsolidado { get; set; }
        [StringLength(2)]
        public string Mes { get; set; }
        [StringLength(4)]
        public string Ano { get; set; }
        public int? Cliente { get; set; }
        [StringLength(50)]
        public string NomeReporting { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public byte? IndEnvio { get; set; }
    }
}

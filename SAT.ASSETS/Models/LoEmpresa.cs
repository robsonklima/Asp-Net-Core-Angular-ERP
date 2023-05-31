using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoEmpresa")]
    public partial class LoEmpresa
    {
        public double? Empresa { get; set; }
        [Column("descricao")]
        [StringLength(255)]
        public string Descricao { get; set; }
        public int? CodFilialSat { get; set; }
    }
}

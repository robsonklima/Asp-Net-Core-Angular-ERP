using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AuditoriaVeiculoTanque")]
    public partial class AuditoriaVeiculoTanque
    {
        [Key]
        public int CodAuditoriaVeiculoTanque { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
    }
}

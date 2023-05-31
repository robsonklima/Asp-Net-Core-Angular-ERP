using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AuditoriaVeiculo")]
    public partial class AuditoriaVeiculo
    {
        [Key]
        public int CodAuditoriaVeiculo { get; set; }
        [StringLength(50)]
        public string Placa { get; set; }
        [StringLength(50)]
        public string Odometro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? CodAuditoriaVeiculoTanque { get; set; }
    }
}

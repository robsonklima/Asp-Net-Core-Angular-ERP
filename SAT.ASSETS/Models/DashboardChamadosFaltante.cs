using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class DashboardChamadosFaltante
    {
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? CodTecnico { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(200)]
        public string Cliente { get; set; }
        public int? CodPeca { get; set; }
        [StringLength(30)]
        public string CodMagnus { get; set; }
        [StringLength(100)]
        public string NomePeca { get; set; }
        [StringLength(200)]
        public string DescStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEstoqueTecnico2
    {
        [Column("codtecnico")]
        public int Codtecnico { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("codfilial")]
        public int? Codfilial { get; set; }
        [Column("cpflogix")]
        [StringLength(20)]
        public string Cpflogix { get; set; }
        [Column("codmagnus")]
        [StringLength(50)]
        public string Codmagnus { get; set; }
        [Column("datainicial", TypeName = "datetime")]
        public DateTime? Datainicial { get; set; }
        [Column("datafinal", TypeName = "datetime")]
        public DateTime? Datafinal { get; set; }
        [Column("qtdepeca", TypeName = "decimal(38, 2)")]
        public decimal? Qtdepeca { get; set; }
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
    }
}

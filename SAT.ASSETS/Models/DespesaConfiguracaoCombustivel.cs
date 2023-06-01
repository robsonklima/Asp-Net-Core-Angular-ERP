using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaConfiguracaoCombustivel")]
    public partial class DespesaConfiguracaoCombustivel
    {
        [Key]
        public int CodDespesaConfiguracaoCombustivel { get; set; }
        public int? CodFilial { get; set; }
        public int? CodUf { get; set; }
        public double? PrecoLitro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DespesaCartaoCombustivelTecnico")]
    public partial class DespesaCartaoCombustivelTecnico
    {
        public int? CodDespesaCartaoCombustivel { get; set; }
        [StringLength(10)]
        public string CodTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraInicio { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

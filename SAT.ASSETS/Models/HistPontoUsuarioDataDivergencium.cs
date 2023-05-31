using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class HistPontoUsuarioDataDivergencium
    {
        [Key]
        public int CodHistPontoUsuarioDataDivergencia { get; set; }
        public int? CodPontoUsuarioDataDivergencia { get; set; }
        public int? CodPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioDataModoDivergencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public int? CodPontoUsuarioDataMotivoDivergencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRegistro { get; set; }
        [StringLength(1)]
        public string Operacao { get; set; }
        [StringLength(1)]
        public string TipoTrigger { get; set; }
    }
}

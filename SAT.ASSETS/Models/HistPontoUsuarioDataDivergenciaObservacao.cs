using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPontoUsuarioDataDivergenciaObservacao")]
    public partial class HistPontoUsuarioDataDivergenciaObservacao
    {
        [Key]
        public int CodHistPontoUsuarioDataDivergenciaObservacao { get; set; }
        public int? CodPontoUsuarioDataDivergenciaObservacao { get; set; }
        public int? CodPontoUsuarioDataDivergencia { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(1)]
        public string Operacao { get; set; }
        [StringLength(1)]
        public string TipoTrigger { get; set; }
    }
}

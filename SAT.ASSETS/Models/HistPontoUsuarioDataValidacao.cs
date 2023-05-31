using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPontoUsuarioDataValidacao")]
    public partial class HistPontoUsuarioDataValidacao
    {
        [Key]
        public int CodHistPontoUsuarioDataValidacao { get; set; }
        public int? CodPontoUsuarioDataValidacao { get; set; }
        public int? CodPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioDataJustificativaValidacao { get; set; }
        public string Observacao { get; set; }
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

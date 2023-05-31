using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class HistPontoUsuarioDatum
    {
        [Key]
        public int CodHistPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioData { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int? CodPontoPeriodo { get; set; }
        public int? CodPontoUsuarioDataStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DataRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        public int? CodPontoUsuarioDataStatusAcesso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(1)]
        public string Operacao { get; set; }
        [StringLength(1)]
        public string TipoTrigger { get; set; }
    }
}

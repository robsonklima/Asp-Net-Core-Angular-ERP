using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPontoUsuario")]
    public partial class HistPontoUsuario
    {
        [Key]
        public int CodHistPontoUsuario { get; set; }
        public int? CodPontoUsuario { get; set; }
        public int? CodPontoPeriodo { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvio { get; set; }
        public byte? IndAprovado { get; set; }
        public byte? IndRevisado { get; set; }
        public byte? IndAtivo { get; set; }
        public string Observacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioAprov { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(1)]
        public string Operacao { get; set; }
        [StringLength(1)]
        public string TipoTrigger { get; set; }
    }
}

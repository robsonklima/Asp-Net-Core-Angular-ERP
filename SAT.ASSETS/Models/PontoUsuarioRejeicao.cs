using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioRejeicao")]
    public partial class PontoUsuarioRejeicao
    {
        [Key]
        public int CodPontoUsuarioRejeicao { get; set; }
        public int? CodRetorno { get; set; }
        public int? CodPontoPeriodo { get; set; }
        [StringLength(1000)]
        public string ChaveSeguranca { get; set; }
        [StringLength(1000)]
        public string ImeiCriptografado { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
        public byte? IndAprovado { get; set; }
    }
}

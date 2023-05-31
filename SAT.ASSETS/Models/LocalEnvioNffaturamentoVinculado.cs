using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LocalEnvioNFFaturamentoVinculado")]
    public partial class LocalEnvioNffaturamentoVinculado
    {
        [Key]
        [Column("CodLocalEnvioNFFaturamento")]
        public int CodLocalEnvioNffaturamento { get; set; }
        [Key]
        public int CodPosto { get; set; }
        [Key]
        public int CodContrato { get; set; }
        public byte? IndAdicionado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.LocalEnvioNffaturamentoVinculados))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodLocalEnvioNffaturamento))]
        [InverseProperty(nameof(LocalEnvioNffaturamento.LocalEnvioNffaturamentoVinculados))]
        public virtual LocalEnvioNffaturamento CodLocalEnvioNffaturamentoNavigation { get; set; }
    }
}

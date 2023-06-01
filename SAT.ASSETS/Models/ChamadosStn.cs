using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChamadosSTN")]
    public partial class ChamadosStn
    {
        [Key]
        public int CodAtendimento { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DataHoraAberturaSTN", TypeName = "datetime")]
        public DateTime DataHoraAberturaStn { get; set; }
        [Column("DataHoraFechamentoSTN", TypeName = "datetime")]
        public DateTime? DataHoraFechamentoStn { get; set; }
        [Column("CodStatusSTN")]
        public int CodStatusStn { get; set; }
        [StringLength(20)]
        public string CodTipoCausa { get; set; }
        [StringLength(20)]
        public string CodGrupoCausa { get; set; }
        [StringLength(20)]
        public string CodDefeito { get; set; }
        [StringLength(20)]
        public string CodCausa { get; set; }
        public int? CodAcao { get; set; }
        [StringLength(50)]
        public string CodTecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column("CodOrigemChamadoSTN")]
        public int? CodOrigemChamadoStn { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        [Column("numReincidenciaAoAssumir")]
        public int? NumReincidenciaAoAssumir { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column("numTratativas")]
        public int? NumTratativas { get; set; }
        [Column("indEvitaPendencia")]
        public int? IndEvitaPendencia { get; set; }
        [Column("indPrimeiraLigacao")]
        public int? IndPrimeiraLigacao { get; set; }
        [StringLength(50)]
        public string NomeSolicitante { get; set; }
        [StringLength(500)]
        public string ObsSistema { get; set; }
    }
}

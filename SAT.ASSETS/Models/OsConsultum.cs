using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OS_Consulta")]
    public partial class OsConsultum
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("NumOSQuarteirizada")]
        [StringLength(20)]
        public string NumOsquarteirizada { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeAutorizada { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasiaCliente { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("NomeEquipSNS")]
        [StringLength(50)]
        public string NomeEquipSns { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(3701)]
        public string DefeitoRelatado { get; set; }
        public string Observ { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        [StringLength(200)]
        public string MotivoCancelamento { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [Column("RAT")]
        [StringLength(20)]
        public string Rat { get; set; }
        [StringLength(50)]
        public string StatusRat { get; set; }
        [StringLength(50)]
        public string Servico { get; set; }
        [StringLength(50)]
        public string TecnicoTransf { get; set; }
        [StringLength(50)]
        public string Tecnico { get; set; }
        [Column("RG")]
        [StringLength(20)]
        public string Rg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InicioAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UltimoAtendimento { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Column("ObsRAT")]
        [StringLength(1000)]
        public string ObsRat { get; set; }
        [StringLength(50)]
        public string NomeTipoCausa { get; set; }
        public int? CodCausa { get; set; }
        [StringLength(70)]
        public string NomeCausa { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        [StringLength(50)]
        public string NomeAcao { get; set; }
        [StringLength(24)]
        public string CodMagnus { get; set; }
        [StringLength(80)]
        public string NomePeca { get; set; }
        public int? QtdePecas { get; set; }
        [Column("A_P")]
        public int? AP { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Required]
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
        [Required]
        [StringLength(1)]
        public string PontoEstrategico { get; set; }
        public byte Rhorario { get; set; }
        [Column("RAcesso")]
        public byte Racesso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [StringLength(1000)]
        public string ObsMotivoCancelamento { get; set; }
        [StringLength(10)]
        public string TensaoSemCarga { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTrocaAgClienteParaAberto { get; set; }
    }
}

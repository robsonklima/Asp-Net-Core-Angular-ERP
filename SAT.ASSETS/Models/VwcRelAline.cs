using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelAline
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSQuarteirizada")]
        [StringLength(20)]
        public string NumOsquarteirizada { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("DescNumSerieNI")]
        [StringLength(20)]
        public string DescNumSerieNi { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [StringLength(50)]
        public string NomeFantasiaCliente { get; set; }
        [StringLength(3)]
        public string NumBanco { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [Column("NomeEquipSNS")]
        [StringLength(50)]
        public string NomeEquipSns { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(4000)]
        public string DataAgendamento { get; set; }
        [Column("DataHoraAberturaOS")]
        [StringLength(4000)]
        public string DataHoraAberturaOs { get; set; }
        [StringLength(4000)]
        public string DataHoraSolicitacao { get; set; }
        [StringLength(4000)]
        public string DataHoraFechamento { get; set; }
        [StringLength(50)]
        public string NomeTipoCausa { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        public string Observ { get; set; }
        public byte? IndCancelado { get; set; }
        [StringLength(4000)]
        public string DataHoraCancelamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [StringLength(50)]
        public string NomeServico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("RG")]
        [StringLength(20)]
        public string Rg { get; set; }
        [StringLength(4000)]
        public string DataHoraInicio { get; set; }
        [StringLength(4000)]
        public string DataHoraSolucao { get; set; }
        [StringLength(4000)]
        public string DataFimGarantia { get; set; }
        public short? QtdeHorasTecnicas { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Column("ObsRAT")]
        [StringLength(1000)]
        public string ObsRat { get; set; }
        [Column("CodRATDetalhe")]
        public int? CodRatdetalhe { get; set; }
        public int? CodCausa { get; set; }
        [StringLength(70)]
        public string NomeCausa { get; set; }
        public int? CodDefeito { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        public int? CodAcao { get; set; }
        [StringLength(50)]
        public string NomeAcao { get; set; }
        [Column("CodRATDetalhesPecas")]
        public int? CodRatdetalhesPecas { get; set; }
        [StringLength(24)]
        public string CodMagnus { get; set; }
        [StringLength(80)]
        public string NomePeca { get; set; }
        public int? QtdePecas { get; set; }
        [Column("A_P")]
        public int? AP { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [Column("StatusRAT")]
        [StringLength(50)]
        public string StatusRat { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Column("TecnicoOS")]
        [StringLength(50)]
        public string TecnicoOs { get; set; }
        public int? CodTecnico { get; set; }
        [Column("TecnicoRAT")]
        [StringLength(50)]
        public string TecnicoRat { get; set; }
        [Column("SEMAT")]
        public byte Semat { get; set; }
        [Required]
        [StringLength(1)]
        public string PontoEstrategico { get; set; }
        public byte Rhorario { get; set; }
        [Column("RAcesso")]
        public byte Racesso { get; set; }
    }
}

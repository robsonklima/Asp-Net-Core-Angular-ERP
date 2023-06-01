using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRat
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodTecnico { get; set; }
        public int? CodStatusServico { get; set; }
        [Required]
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [StringLength(50)]
        public string NomeRespCliente { get; set; }
        [StringLength(50)]
        public string NomeAcompanhante { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraChegada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFim { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolucao { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Column("ObsRAT")]
        [StringLength(1000)]
        public string ObsRat { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodServico { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAbertura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolicitacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraReparo { get; set; }
        public short? QtdeHorasInicio { get; set; }
        public short? QtdeHorasReparo { get; set; }
        public short? QtdeHorasSolucao { get; set; }
        public short? QtdeHorasEspera { get; set; }
        [StringLength(1000)]
        public string MotivoEspera { get; set; }
        public short? QtdeHorasInterrupcao { get; set; }
        [StringLength(1000)]
        public string MotivoInterrupcao { get; set; }
        public short? QtdeHorasTecnicas { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValServicos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaReparo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaSolucao { get; set; }
        public float? TempoEfetInicio { get; set; }
        public float? TempoEfetReparo { get; set; }
        public float? TempoEfetSolucao { get; set; }
        [Column("IndBRBAtendeConfederal")]
        [StringLength(1)]
        public string IndBrbatendeConfederal { get; set; }
        [Column("IndRATDigitalizada")]
        public byte? IndRatdigitalizada { get; set; }
        [Column("CaminhoRATDigitalizada")]
        [StringLength(80)]
        public string CaminhoRatdigitalizada { get; set; }
        public byte? IndQuarentena { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraInicioValida { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolucaoValida { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamentoValida { get; set; }
        [Column("VentNotaBRB")]
        public byte? VentNotaBrb { get; set; }
        [Column("RepCasBRB")]
        public byte? RepCasBrb { get; set; }
        [StringLength(50)]
        public string NomeTecnico { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
    }
}

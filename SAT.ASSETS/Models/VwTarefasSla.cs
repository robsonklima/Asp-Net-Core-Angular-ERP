using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasSla
    {
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Column("tituloTarefa")]
        public string TituloTarefa { get; set; }
        [Required]
        [Column("codUsuario")]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column("dataHoraCadastro", TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        [Column("dataHoraPriorizacao", TypeName = "datetime")]
        public DateTime? DataHoraPriorizacao { get; set; }
        [Column("numPriorizacao")]
        public int? NumPriorizacao { get; set; }
        [Column("complexidade")]
        public int? Complexidade { get; set; }
        [Column("urgenciaUsuario")]
        public int? UrgenciaUsuario { get; set; }
        [Column("codTarefaStatus")]
        public int CodTarefaStatus { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(25)]
        public string NomeUsuario { get; set; }
        [Column("codTarefaTipo")]
        public int CodTarefaTipo { get; set; }
        [Column("nomeTarefaTipo")]
        [StringLength(50)]
        public string NomeTarefaTipo { get; set; }
        [Required]
        [Column("prioridade")]
        [StringLength(13)]
        public string Prioridade { get; set; }
        [Column("descricaotarefa")]
        public string Descricaotarefa { get; set; }
        [Required]
        [Column("relatosolucao")]
        public string Relatosolucao { get; set; }
        [Column("duracao")]
        public int? Duracao { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Required]
        [Column("nometipoduracao")]
        [StringLength(13)]
        public string Nometipoduracao { get; set; }
        [Required]
        [Column("nometiposolucao")]
        [StringLength(25)]
        public string Nometiposolucao { get; set; }
        [Required]
        [Column("dataencerramento")]
        [StringLength(20)]
        public string Dataencerramento { get; set; }
        [Column("nomeTarefaModulo")]
        [StringLength(50)]
        public string NomeTarefaModulo { get; set; }
        [Column("previsao")]
        public double? Previsao { get; set; }
        [Column("tipoPrevisao")]
        public int? TipoPrevisao { get; set; }
        [Column("tipoSolucao")]
        [StringLength(5)]
        public string TipoSolucao { get; set; }
        [Column("codTarefaModulo")]
        public int? CodTarefaModulo { get; set; }
        [Column("indLista")]
        public int? IndLista { get; set; }
        [StringLength(50)]
        public string RamalSolicitante { get; set; }
        [Column("descricaoStatus")]
        public string DescricaoStatus { get; set; }
        [Column("numCodTrace")]
        public int? NumCodTrace { get; set; }
        [Column("indTraceGP")]
        public int IndTraceGp { get; set; }
        [Column("tecnologia")]
        [StringLength(3)]
        public string Tecnologia { get; set; }
        [Required]
        [Column("urgência")]
        [StringLength(5)]
        public string Urgência { get; set; }
        [Required]
        [Column("tipo")]
        [StringLength(7)]
        public string Tipo { get; set; }
        public int? TempoDesenv { get; set; }
        [StringLength(83)]
        public string Impacto { get; set; }
        [Column("indResolucaoSAT")]
        public int? IndResolucaoSat { get; set; }
        [Column("dataHoraDefinicaoSLA", TypeName = "datetime")]
        public DateTime? DataHoraDefinicaoSla { get; set; }
        [Column("dataHoraFimSLA", TypeName = "datetime")]
        public DateTime? DataHoraFimSla { get; set; }
        [Column("nometarefastatus")]
        [StringLength(50)]
        public string Nometarefastatus { get; set; }
        [Column("ordem")]
        public int? Ordem { get; set; }
        [Column("codTarefaImpactoGrupo")]
        public int? CodTarefaImpactoGrupo { get; set; }
        [Required]
        [Column("SLA")]
        [StringLength(1)]
        public string Sla { get; set; }
        [StringLength(10)]
        public string Tempo { get; set; }
    }
}

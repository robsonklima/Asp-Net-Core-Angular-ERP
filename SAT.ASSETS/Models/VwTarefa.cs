using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefa
    {
        [Column("nomeTarefaStatus")]
        [StringLength(50)]
        public string NomeTarefaStatus { get; set; }
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Required]
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
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [Column("codTarefaTipo")]
        public int CodTarefaTipo { get; set; }
        [Column("nomeTarefaTipo")]
        [StringLength(50)]
        public string NomeTarefaTipo { get; set; }
        [Required]
        [StringLength(13)]
        public string Prioridade { get; set; }
        [Column("descricaoTarefa")]
        public string DescricaoTarefa { get; set; }
        [Required]
        public string RelatoSolucao { get; set; }
        [Column("duracao")]
        public int? Duracao { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [Column("nomeTipoDuracao")]
        [StringLength(13)]
        public string NomeTipoDuracao { get; set; }
        [Required]
        [Column("nomeTipoSolucao")]
        [StringLength(25)]
        public string NomeTipoSolucao { get; set; }
        [Required]
        [Column("dataEncerramento")]
        [StringLength(20)]
        public string DataEncerramento { get; set; }
        [Required]
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
        [Column("nomePrevisaoSolucao")]
        [StringLength(13)]
        public string NomePrevisaoSolucao { get; set; }
        [Column("previsaoSolucao")]
        public double? PrevisaoSolucao { get; set; }
        [Column("porcentagemConclusao")]
        public int PorcentagemConclusao { get; set; }
        [Column("Previsão TOTAL")]
        [StringLength(7)]
        public string PrevisãoTotal { get; set; }
        [Required]
        [StringLength(5)]
        public string Urgência { get; set; }
        [Required]
        [StringLength(7)]
        public string Tipo { get; set; }
        [StringLength(7)]
        public string Esforço { get; set; }
        [Column("Previsão Análise 30%")]
        [StringLength(7)]
        public string PrevisãoAnálise30 { get; set; }
        [Column("Previsão Análise DRS 20%")]
        [StringLength(7)]
        public string PrevisãoAnáliseDrs20 { get; set; }
        [Column("Previsão Análise DET 10%")]
        [StringLength(7)]
        public string PrevisãoAnáliseDet10 { get; set; }
        [Column("Previsão Desenvolvimento 70%")]
        [StringLength(7)]
        public string PrevisãoDesenvolvimento70 { get; set; }
        [Column("Previsão Teste 15%")]
        [StringLength(7)]
        public string PrevisãoTeste15 { get; set; }
        [Column("Previsão Homologação 3%")]
        [StringLength(7)]
        public string PrevisãoHomologação3 { get; set; }
        [Column("Previsão Implantação 3%")]
        [StringLength(7)]
        public string PrevisãoImplantação3 { get; set; }
        [Column("Horas Faltantes")]
        [StringLength(7)]
        public string HorasFaltantes { get; set; }
        public int? TempoDesenv { get; set; }
        [StringLength(5)]
        public string PorcentImpacto { get; set; }
        [StringLength(50)]
        public string NomeTarefaImpactoGrupo { get; set; }
        [Column("indResolucaoSAT")]
        public int? IndResolucaoSat { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr1 { get; set; }
        [Column("dataHoraDefinicaoSLA", TypeName = "datetime")]
        public DateTime? DataHoraDefinicaoSla { get; set; }
        [Column("dataHoraFimSLA", TypeName = "datetime")]
        public DateTime? DataHoraFimSla { get; set; }
        [Column("codTarefaImpactoGrupo")]
        public int? CodTarefaImpactoGrupo { get; set; }
        [Column("codTarefaImpacto")]
        [StringLength(10)]
        public string CodTarefaImpacto { get; set; }
        [Column("indSLA")]
        public int IndSla { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCargo { get; set; }
        [Required]
        [StringLength(50)]
        public string NomePerfil { get; set; }
        [Column("codAnalista")]
        public int? CodAnalista { get; set; }
        [StringLength(50)]
        public string Analista { get; set; }
    }
}

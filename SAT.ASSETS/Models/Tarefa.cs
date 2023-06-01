using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Tarefa
    {
        public Tarefa()
        {
            TarefaNotificacaos = new HashSet<TarefaNotificacao>();
            TarefasAnexos = new HashSet<TarefasAnexo>();
        }

        [Key]
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Column("codTarefaImpactoGrupo")]
        public int CodTarefaImpactoGrupo { get; set; }
        [Column("codTarefaStatus")]
        public int CodTarefaStatus { get; set; }
        [Column("codTarefaModulo")]
        public int? CodTarefaModulo { get; set; }
        [Required]
        [Column("tituloTarefa")]
        public string TituloTarefa { get; set; }
        [Column("descricaoTarefa")]
        public string DescricaoTarefa { get; set; }
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
        [Column("_modulo")]
        [StringLength(50)]
        public string Modulo { get; set; }
        [Column("complexidade")]
        public int? Complexidade { get; set; }
        [Column("codTarefaTipo")]
        public int CodTarefaTipo { get; set; }
        [Column("urgenciaUsuario")]
        public int? UrgenciaUsuario { get; set; }
        [Column("relatoSolucao")]
        public string RelatoSolucao { get; set; }
        [Column("duracao")]
        public int? Duracao { get; set; }
        [Column("tipoDuracao")]
        public int? TipoDuracao { get; set; }
        [Column("dataEncerramento", TypeName = "datetime")]
        public DateTime? DataEncerramento { get; set; }
        [Column("previsao")]
        public double? Previsao { get; set; }
        [Column("tipoPrevisao")]
        public int? TipoPrevisao { get; set; }
        [Column("tipoSolucao")]
        [StringLength(5)]
        public string TipoSolucao { get; set; }
        [Column("indLista")]
        public int? IndLista { get; set; }
        [Column("descricaoStatus")]
        public string DescricaoStatus { get; set; }
        [StringLength(50)]
        public string RamalSolicitante { get; set; }
        [Column("indTraceGP")]
        public int IndTraceGp { get; set; }
        [Column("numCodTrace")]
        public int? NumCodTrace { get; set; }
        [Column("tecnologia")]
        [StringLength(3)]
        public string Tecnologia { get; set; }
        [Column("porcentagemConclusao")]
        public int PorcentagemConclusao { get; set; }
        [Column("dataHoraFimSLA", TypeName = "datetime")]
        public DateTime? DataHoraFimSla { get; set; }
        [Column("dataHoraDefinicaoSLA", TypeName = "datetime")]
        public DateTime? DataHoraDefinicaoSla { get; set; }
        [Column("indSLA")]
        public int IndSla { get; set; }
        [StringLength(100)]
        public string Autorizador { get; set; }
        [Column("ResponsavelSAT")]
        [StringLength(100)]
        public string ResponsavelSat { get; set; }
        public int? CodAnalista { get; set; }

        [ForeignKey(nameof(CodTarefaModulo))]
        [InverseProperty(nameof(TarefasModulo.Tarefas))]
        public virtual TarefasModulo CodTarefaModuloNavigation { get; set; }
        [ForeignKey(nameof(CodTarefaStatus))]
        [InverseProperty(nameof(TarefasStatus.Tarefas))]
        public virtual TarefasStatus CodTarefaStatusNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.Tarefas))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(TarefaNotificacao.CodTarefaNavigation))]
        public virtual ICollection<TarefaNotificacao> TarefaNotificacaos { get; set; }
        [InverseProperty(nameof(TarefasAnexo.CodTarefaNavigation))]
        public virtual ICollection<TarefasAnexo> TarefasAnexos { get; set; }
    }
}

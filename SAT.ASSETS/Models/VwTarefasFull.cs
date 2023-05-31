using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasFull
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
        [Column("tipoDuracao")]
        [StringLength(13)]
        public string TipoDuracao { get; set; }
        [Required]
        [Column("dataEncerramento")]
        [StringLength(20)]
        public string DataEncerramento { get; set; }
        [Required]
        [Column("nomeTarefaModulo")]
        [StringLength(50)]
        public string NomeTarefaModulo { get; set; }
        public int? Expr1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr2 { get; set; }
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
    }
}

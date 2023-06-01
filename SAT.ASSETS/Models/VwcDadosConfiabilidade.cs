using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDadosConfiabilidade
    {
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(20)]
        public string NroContrato { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        public int? CodCausa { get; set; }
        [Column("CodECausaAbrev")]
        [StringLength(3)]
        public string CodEcausaAbrev { get; set; }
        [StringLength(50)]
        public string Defeito { get; set; }
        [StringLength(50)]
        public string Acao { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Required]
        [Column("DCPosto")]
        [StringLength(2)]
        public string Dcposto { get; set; }
        public int CodEquip { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [StringLength(1)]
        public string Ind { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraDefeito { get; set; }
        [StringLength(92)]
        public string DataDefeito { get; set; }
        [StringLength(30)]
        public string HoraDefeito { get; set; }
        public int? QtdDia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [Column("OS")]
        public int? Os { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolicAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFim { get; set; }
        public double? TempoConserto { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? TempoMaquinaIndisponivel { get; set; }
        [Column("TecnicoRAT")]
        [StringLength(50)]
        public string TecnicoRat { get; set; }
        [StringLength(16)]
        public string FonePerto { get; set; }
        [StringLength(40)]
        public string Celular { get; set; }
        [StringLength(16)]
        public string FoneParticular { get; set; }
        [Column("UsuarioCadastroRAT")]
        [StringLength(50)]
        public string UsuarioCadastroRat { get; set; }
        [StringLength(50)]
        public string Lote { get; set; }
    }
}

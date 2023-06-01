using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelatorioRatsTensaoTerraNeutro
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Required]
        [Column("RAT")]
        [StringLength(20)]
        public string Rat { get; set; }
        [Column("OS")]
        public int Os { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("Tipo_Intervencao")]
        [StringLength(50)]
        public string TipoIntervencao { get; set; }
        [StringLength(50)]
        public string Servico { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Column("Nome_Tecnico")]
        [StringLength(50)]
        public string NomeTecnico { get; set; }
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(10)]
        public string TensaoSemCarga { get; set; }
        [StringLength(10)]
        public string TensaoComCarga { get; set; }
        [StringLength(10)]
        public string TensaoTerraNeutro { get; set; }
        [StringLength(10)]
        public string TemperaturaAmbiente { get; set; }
        public int? IndCedulaBoaQualidade { get; set; }
        public int? IndCedulaVentilada { get; set; }
        public int? IndRedeEstabilizada { get; set; }
        public int? IndInfraEstruturaLogicaAdequada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAbertura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolucao { get; set; }
        public short? QtdeHorasTecnicas { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Column("ObsRAT")]
        [StringLength(1000)]
        public string ObsRat { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBHistEquipamentoContrato")]
    public partial class DispBbhistEquipamentoContrato
    {
        public int CodEquipContrato { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        [Column("CodSLA")]
        public int CodSla { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(20)]
        public string NumSerieCliente { get; set; }
        public int CodCliente { get; set; }
        public int CodPosto { get; set; }
        public int CodRegiao { get; set; }
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        [Column("DistanciaPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaPatRes { get; set; }
        public byte? IndGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        public byte IndReceita { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorReceita { get; set; }
        public byte IndRepasse { get; set; }
        public byte IndRepasseIndividual { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorRepasse { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorDespesa { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte IndInstalacao { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataDesativacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string Origem { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        [StringLength(20)]
        public string CodEquipCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column("IndSEMAT")]
        public byte? IndSemat { get; set; }
        [StringLength(1)]
        public string PontoEstrategico { get; set; }
        [Column("IndRHorario")]
        public byte? IndRhorario { get; set; }
        [Column("IndRAcesso")]
        public byte? IndRacesso { get; set; }
        public int? CodTipoLocalAtendimento { get; set; }
        public byte? IndVerao { get; set; }
        [Column("IndPAE")]
        public byte? IndPae { get; set; }
        public byte? IndRetrofit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetrofit1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetrofit2 { get; set; }
        public byte? IndRetrofit2 { get; set; }
        public byte? IndRetrofit3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetrofit3 { get; set; }
        [Column("IndMTBF4")]
        public byte? IndMtbf4 { get; set; }
        [StringLength(20)]
        public string NumEtiquetaEquip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRelatorio { get; set; }
        public byte? IndRelatorio { get; set; }
        [Column("CodBMP")]
        public string CodBmp { get; set; }
        public string Sequencia { get; set; }
        public byte? IndMecanismo { get; set; }
        [Column("CodDispBBCriticidade")]
        public int? CodDispBbcriticidade { get; set; }
    }
}

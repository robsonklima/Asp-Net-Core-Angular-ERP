using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("PecaTeste")]
    public partial class PecaTeste
    {
        public int CodPeca { get; set; }
        [Required]
        [StringLength(24)]
        public string CodMagnus { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus { get; set; }
        public int? CodTraducao { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCusto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCustoDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCustoEuro { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPecaDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaEuro { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaAssistencia { get; set; }
        [Column("ValIPIAssistencia", TypeName = "decimal(10, 2)")]
        public decimal? ValIpiassistencia { get; set; }
        [Column("Descr_Ingles")]
        [StringLength(80)]
        public string DescrIngles { get; set; }
        public int IndObrigRastreabilidade { get; set; }
        public int IndValorFixo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAtualizacaoValor { get; set; }
        public int IsValorAtualizado { get; set; }
        [Column("NCM")]
        [StringLength(10)]
        public string Ncm { get; set; }
        public byte? ListaBackup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtObsoleto { get; set; }
        [Column("UtilizadoDSS")]
        public byte? UtilizadoDss { get; set; }
        public byte? ItemLogix { get; set; }
        public int? HierarquiaPesquisa { get; set; }
        public double? IndiceDeTroca { get; set; }
        public byte? KitTecnico { get; set; }
        [Column("QTDPecaKitTecnico")]
        public int? QtdpecaKitTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataIntegracaoLogix { get; set; }
    }
}

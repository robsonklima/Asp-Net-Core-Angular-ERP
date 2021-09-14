using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Peca
    {
        [Key]
        public int CodPeca { get; set; }
        public string CodMagnus { get; set; }
        public string NomePeca { get; set; }
        public decimal ValCusto { get; set; }
        public decimal ValCustoDolar { get; set; }
        public decimal? ValCustoEuro { get; set; }
        public decimal ValPeca { get; set; }
        public decimal ValPecaDolar { get; set; }
        public decimal? ValPecaEuro { get; set; }
        public decimal? ValPecaAssistencia { get; set; }
        public decimal? ValIpiassistencia { get; set; }
        public decimal ValIPI { get; set; }
        public int QtdMinimaVenda { get; set; }
        public string Ncm { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? CodPecaFamilia { get; set; }
        [ForeignKey("CodPecaFamilia")]
        public PecaFamilia PecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus { get; set; }
        [ForeignKey("CodPecaStatus")]
        public PecaStatus PecaStatus { get; set; }
        public int IndObrigRastreabilidade { get; set; }
        public int IndValorFixo { get; set; }
        public DateTime? DataHoraAtualizacaoValor { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IsValorAtualizado { get; set; }
        public byte? ListaBackup { get; set; }
        public string DtObsoleto { get; set; }
        public byte? UtilizadoDss { get; set; }
        public byte? ItemLogix { get; set; }
        public int? HierarquiaPesquisa { get; set; }
        public double? IndiceDeTroca { get; set; }
        public byte? KitTecnico { get; set; }
        public int? QtdpecaKitTecnico { get; set; }
        public DateTime? DataIntegracaoLogix { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
using System;

namespace SAT.MODELS.Entities
{
    public class PecasLaboratorio
    {
        public int CodPeca { get; set; }
        public string CodMagnus { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus  { get; set; }
        public int? CodTraducao { get; set; }
        public string NomePeca { get; set; }
        public decimal ValCusto { get; set; }
        public decimal ValCustoDolar { get; set; }
        public decimal? ValCustoEuro { get; set; }
        public decimal ValPeca { get; set; }
        public decimal ValPecaDolar { get; set; }
        public decimal? ValPecaEuro { get; set; }
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public decimal? ValPecaAssistencia { get; set; }
        public decimal? ValIpiassistencia { get; set; }
        public string Descr_Ingles { get; set; }
        public int IndObrigRastreabilidade { get; set; }
        public int IndValorFixo { get; set; }
        public DateTime? DataHoraAtualizacaoValor { get; set; }
        public int IsValorAtualizado { get; set; }
        public string Ncm { get; set; }
        public int? CodChecklist { get; set; }
        public int? Quantidade { get; set; }
        public Peca Peca { get; set; }
        public PecaFamilia PecaFamilia { get; set; }
        public PecaStatus PecaStatus { get; set; }
        public ORCheckList ORCheckList { get; set; }

    }
}
using System;

namespace SAT.MODELS.Entities
{
    public class ImportacaoInstalacao : ImportacaoBase
    {
        public string CodInstalacao { get; set; }
        public string NumSerie { get; set; }
        public string BemTradeIn { get; set; }
        public string NfVenda { get; set; }
        public string NfVendaData { get; set; }
        public string Nfremessa { get; set; }
        public string DataNfremessa { get; set; }
        public string DataExpedicao { get; set; }
        public string NomeTransportadora { get; set; }
        public string DataSugEntrega { get; set; }
        public string DataConfEntrega { get; set; }
        public string DataConfInstalacao { get; set; }
        public string NomeRespBancoBt { get; set; }
        public string NumMatriculaBt { get; set; }
    }
}
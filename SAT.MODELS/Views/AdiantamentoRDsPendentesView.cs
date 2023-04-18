using System;

namespace SAT.MODELS.Views
{
    public class AdiantamentoRDsPendentesView
    {
        public int CodTecnico { get; set; }
        public string Tecnico { get; set; }
        public int NroRD { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public decimal? TotalRD { get; set; }
        public decimal? Despesas { get; set; }
        public decimal? Adiantamento { get; set; }
        public int Reembolso { get; set; }
        public decimal SaldoAdiantamentoSAT { get; set; }
        public string Protocolo { get; set; }
        public string DtEnvioProtocolo { get; set; }
        public string Situacao { get; set; }
        public string Controladoria { get; set; }
        public string Cor { get; set; }
        public string NomeDespesaPeriodoTecnicoStatus { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataInicio2 { get; set; }
    }
}
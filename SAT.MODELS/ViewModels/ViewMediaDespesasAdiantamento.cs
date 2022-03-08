namespace SAT.MODELS.ViewModels {
    public class ViewMediaDespesasAdiantamento
    {
        public int CodTecnico { get; set; }
        public string Tecnico { get; set; }
        public string Cpf { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string EmailDefault { get; set; }
        public decimal? MediaMensal { get; set; }
        public decimal? MediaQuinzenal { get; set; }
        public decimal? MediaSemanal { get; set; }
        public int SaldoAbertoLogixMensal { get; set; }
        public int SaldoAbertoLogixQuinzenal { get; set; }
        public int SaldoAbertoLogixSemanal { get; set; }
        public decimal? RdsEmAbertoMensal { get; set; }
        public decimal? RdsEmAbertoQuinzenal { get; set; }
        public decimal? RdsEmAbertoSemanal { get; set; }
        public int SaldoAdiantamentoSatmensal { get; set; }
        public int SaldoAdiantamentoSatquinzenal { get; set; }
        public int SaldoAdiantamentoSatsemanal { get; set; }
        public decimal? MaximoParaSolicitarMensal { get; set; }
        public decimal? MaximoParaSolicitarQuinzenal { get; set; }
        public decimal? MaximoParaSolicitarSemanal { get; set; }
    }
}
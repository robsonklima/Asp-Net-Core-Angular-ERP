namespace SAT.MODELS.Entities
{
    public class OrdemServicoBB
    {
        public string NumOSCliente { get; set; }
        public string TipoOS { get; set; }
        public string NumAgencia { get; set; }
        public string DCPosto { get; set; }
        public string NomeAgencia { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string DDDDependencia { get; set; }
        public string Telefone { get; set; }
        public string CGC { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Criticidade { get; set; }
        public string DescCriticidade { get; set; }
        public string NumBem { get; set; }
        public string DescricaoBem { get; set; }
        public string NumSerie { get; set; }
        public string Modelo { get; set; }
        public string BilheteOS { get; set; }
        public string GarantiaBem { get; set; }
        public string Impacto { get; set; }
        public string DescricaoImpacto { get; set; }
        public string Defeito { get; set; }
        public string TipoManutencao { get; set; }
        public string DescricaoManutencao { get; set; }
        public string CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string MatriculaAberturaOS { get; set; }
        public string DataAberturaOS { get; set; }
        public string HoraAberturaOS { get; set; }
        public string NomeContato { get; set; }
        public string NumeroChamada { get; set; }
        public string DataChamadaOS { get; set; }
        public string HoraChamadaOS { get; set; }
        public string ChaveDoChamador { get; set; }
        public string NumeroDiasAtendimento { get; set; }
        public string NumeroHorasAtendimento { get; set; }
        public string NumeroContrato { get; set; }
        public string CodigoMantenedora { get; set; }
        public string IndOSGarantia { get; set; }
        public string DataAgendaOS { get; set; }
        public string HoraAgendaOS { get; set; }
        public string PrefixoDependenciaDestino { get; set; }
        public string SubordinadaDependenciaDestino { get; set; }
        public string CodigoTipoBem { get; set; }
        public string TextoMotivoManutencao { get; set; }
    }
}
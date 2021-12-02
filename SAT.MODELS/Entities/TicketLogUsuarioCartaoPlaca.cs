using System;

namespace SAT.MODELS.Entities
{
    public class TicketLogUsuarioCartaoPlaca
    {
        public int CodTicketLogUsuarioCartaoPlaca { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeResponsavel { get; set; }
        public string Placa { get; set; }
        public string VeiculoCidade { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public string Situacao { get; set; }
        public string VeiculoUF { get; set; }
        public string DescricaoTipoCombustivel { get; set; }
        public string DescricaoTipoFrota { get; set; }
        public double? ValorReservado { get; set; }
        public int? CodigoGrupoRestricao { get; set; }
        public double? Saldo { get; set; }
        public string DescricaoModeloVeiculo { get; set; }
        public string Temporario { get; set; }
        public string VeiculoFabricante { get; set; }
        public double? Compras { get; set; }
        public string ControlaHodometro { get; set; }
        public double? LimiteAtual { get; set; }
        public int? CodigoUsuarioCartao { get; set; }
        public string SituacaoVeiculo { get; set; }
        public string TrackOnline { get; set; }
        public string ControlaHorimetro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
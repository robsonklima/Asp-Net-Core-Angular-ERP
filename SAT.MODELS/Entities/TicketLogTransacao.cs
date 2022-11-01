using System;

namespace SAT.MODELS.Entities
{
    public class TicketLogTransacao
    {
        public int CodTicketLogTransacao { get; set; }
        public double QuilometrosPorLitro { get; set; }
        public int CodigoTransacao { get; set; }
        public string Cor { get; set; }
        public double ValorTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public int CodigoFamiliaVeiculo { get; set; }
        public string NumeroCartao { get; set; }
        public string Principal { get; set; }
        public string VeiculoFabricante { get; set; }
        public string Uf { get; set; }
        public string CnpjEstabelecimento { get; set; }
        public string SegmentoVeiculo { get; set; }
        public string ControlaHodometro { get; set; }
        public string GrupoRestricaoVeiculo { get; set; }
        public decimal Litros { get; set; }
        public int CodigoLiberacaoRestricao { get; set; }
        public string GrupoRestricaoTransacao { get; set; }
        public string DescricaoSeriePos { get; set; }
        public int QuilometrosRodados { get; set; }
        public string Responsavel { get; set; }
        public string Placa { get; set; }
        public double ValorSaldoAnterior { get; set; }
        public int Ano { get; set; }
        public int CodigoOrdemServico { get; set; }
        public string TipoCombustivel { get; set; }
        public string NomeCidade { get; set; }
        public string NumeroTerminal { get; set; }
        public string CodigoServico { get; set; }
        public string ExibeMediaQuilometragem { get; set; }
        public string NomeMotorista { get; set; }
        public string NomeReduzidoEstabelecimento { get; set; }
        public string TipoFrota { get; set; }
        public string VeiculoModelo { get; set; }
        public string ControleDesempenho { get; set; }
        public int CodigoVeiculoCliente { get; set; }
        public int CodigoTipoCombustivel { get; set; }
        public int NumeroMatricula { get; set; }
        public bool QuilometragemInicial { get; set; }
        public double ValorLitro { get; set; }
        public int CodigoUsuarioCartao { get; set; }
        public int CodigoEstabelecimento { get; set; }
        public int Quilometragem { get; set; }
        public string Servico { get; set; }
        public int CodigoServicoOrdemServico { get; set; }
        public string ControlaHorimetro { get; set; }
        public DateTime DataHoraConsulta { get; set; }
    }
}
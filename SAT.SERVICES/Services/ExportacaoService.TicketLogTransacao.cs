using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaTicketLogTransacao(TicketLogTransacaoParameters parameters)
		{
            var data = _ticketLogTransacaoRepo.ObterPorParametros(parameters);
            var sheet = data.Select(d =>
                            new
                            {
                                CodigoSAT = d.CodTicketLogTransacao,
                                QuilometrosPorLitro = d.QuilometrosPorLitro,
                                CodigoTransacao = d.CodigoTransacao,
                                Cor = d.Cor,
                                ValorTransacao = d.ValorTransacao,
                                DataTransacao = d.DataTransacao,
                                CodigoFamiliaVeiculo = d.CodigoFamiliaVeiculo,
                                NumeroCartao = d.NumeroCartao,
                                VeiculoFabricante = d.VeiculoFabricante,
                                Uf = d.Uf,
                                CnpjEstabelecimento = d.CnpjEstabelecimento,
                                SegmentoVeiculo = d.SegmentoVeiculo,
                                ControlaHodometro = d.ControlaHodometro,
                                Litros = d.Litros,
                                CodigoLiberacaoRestricao = d.CodigoLiberacaoRestricao,
                                GrupoRestricaoTransacao = d.GrupoRestricaoTransacao,
                                DescricaoSeriePos = d.DescricaoSeriePos,
                                QuilometrosRodados = d.QuilometrosRodados,
                                Placa = d.Placa,
                                ValorSaldoAnterior = d.ValorSaldoAnterior,
                                Ano = d.Ano,
                                CodigoOrdemServico = d.CodigoOrdemServico,
                                TipoCombustivel = d.TipoCombustivel,
                                NomeCidade = d.NomeCidade,
                                NumeroTerminal = d.NumeroTerminal,
                                CodigoServico = d.CodigoServico,
                                ExibeMediaQuilometragem = d.ExibeMediaQuilometragem,
                                NomeMotorista = d.NomeMotorista,
                                NomeReduzidoEstabelecimento = d.NomeReduzidoEstabelecimento,
                                TipoFrota = d.TipoFrota,
                                VeiculoModelo = d.VeiculoModelo,
                                ControleDesempenho = d.ControleDesempenho,
                                CodigoVeiculoCliente = d.CodigoVeiculoCliente,
                                CodigoTipoCombustivel = d.CodigoTipoCombustivel,
                                NumeroMatricula = d.NumeroMatricula,
                                QuilometragemInicial = d.QuilometragemInicial,
                                ValorLitro = d.ValorLitro,
                                CodigoUsuarioCartao = d.CodigoUsuarioCartao,
                                CodigoEstabelecimento = d.CodigoEstabelecimento,
                                CodQuilometragemigo = d.Quilometragem,
                                CodigoServicoOrdemServico = d.CodigoServicoOrdemServico,
                                ControlaHorimetro = d.ControlaHorimetro,
                                DataHoraConsulta = d.DataHoraConsulta
                            });

            var wsOs = Workbook.Worksheets.Add("ticket-log-transacoes");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}
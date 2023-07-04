using System;
using System.IO;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities;
using NLog;
using NLog.Fluent;
using SAT.MODELS.Entities.Constants;
using System.Globalization;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public class IntegracaoMRPService : IIntegracaoMRPService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMRPLogixService _mrpLogixService;
        private readonly IMRPLogixEstoqueService _mrpLogixEstoqueService; 

        public IntegracaoMRPService(
           IMRPLogixService mrpLogixService,
           IMRPLogixEstoqueService mrpLogixEstoqueService
       )
        {
            _mrpLogixService = mrpLogixService;
            _mrpLogixEstoqueService = mrpLogixEstoqueService;
        }
        public void ImportarArquivoMRPLogix()
        {
            try
            {
                _mrpLogixService.LimparTabela();
                
                var arquivos = GenericHelper.LerDiretorioInput(Constants.PEDIDOS_PENDENTES);

                foreach (var arquivo in arquivos)
                {
                    MRPLogix mrpLogix = new();
                    string[] dados = arquivo.Split('|');

                    if (dados.Length != 24)
                        _logger.Error("A quantidade de campos encontrados é diferente do permitido");

                    mrpLogix.NumPedido = dados[0].ToString();
                    mrpLogix.DataPedido = DateTime.Parse(dados[1].ToString());
                    mrpLogix.Nomecliente = dados[2].ToString();
                    mrpLogix.CodItem = dados[3].ToString();
                    mrpLogix.NomeItem = dados[4].ToString();
                    mrpLogix.CodCliente = dados[5].ToString();
                    mrpLogix.QtdPedido = Double.Parse(dados[6].ToString(), new CultureInfo("en-US"));
                    mrpLogix.QtdSolicitada = Double.Parse(dados[7].ToString(), new CultureInfo("en-US"));
                    mrpLogix.LocalProd = dados[8].ToString();
                    mrpLogix.QtdCancelada = Double.Parse(dados[9].ToString(), new CultureInfo("en-US"));
                    mrpLogix.Preco = Double.Parse(dados[10].ToString(), new CultureInfo("en-US"));
                    mrpLogix.QtdAtendida = Double.Parse(dados[11].ToString(), new CultureInfo("en-US"));
                    mrpLogix.PrazoEntrega = DateTime.Parse(dados[12].ToString());
                    mrpLogix.DiasPedido = Int32.Parse(dados[13].ToString());
                    mrpLogix.CodEmpresa = Int32.Parse(dados[14].ToString());
                    mrpLogix.LocalEstoque = dados[15].ToString();
                    mrpLogix.NumSequencia = Double.Parse(dados[16].ToString(), new CultureInfo("en-US"));
                    mrpLogix.IPI = Double.Parse(dados[17].ToString(), new CultureInfo("en-US"));
                    mrpLogix.CodUsuario = dados[18].ToString();
                    mrpLogix.Tipo = dados[19].ToString();
                    mrpLogix.SaldoTotal = dados[20].ToString() == "" ? 0 : Double.Parse(dados[20].ToString(), new CultureInfo("en-US"));
                    mrpLogix.NumSequenciaPed = Double.Parse(dados[21].ToString(), new CultureInfo("en-US"));
                    mrpLogix.Saldo = Double.Parse(dados[22].ToString(), new CultureInfo("en-US"));

                    _mrpLogixService.Criar(mrpLogix);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao ler o arquivo: " + ex.Message);
            }
        }

        public void ImportarArquivoMRPEstoqueLogix()
        {
            try
            {
                _mrpLogixEstoqueService.LimparTabela();

                var arquivos = GenericHelper.LerDiretorioInput(Constants.ESTOQUE_LOTE);

                foreach (var arquivo in arquivos)
                {
                    MRPLogixEstoque mrpLogixEstoque = new();
                    string[] dados = arquivo.Split('|');

                    if (dados.Length != 7)
                        _logger.Error("A quantidade de campos encontrados é diferente do permitido");

                    mrpLogixEstoque.CodEmpresa = dados[0].ToString();
                    mrpLogixEstoque.CodItem = dados[1].ToString();
                    mrpLogixEstoque.LocalEstoque = dados[2].ToString();
                    mrpLogixEstoque.NumLote = dados[3].ToString();
                    mrpLogixEstoque.Situacao = dados[4].ToString();
                    mrpLogixEstoque.QtdSaldo = Double.Parse(dados[5].ToString(), new CultureInfo("en-US"));
                    mrpLogixEstoque.NumTransacao = Int32.Parse(dados[6].ToString());

                    _mrpLogixEstoqueService.Criar(mrpLogixEstoque);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
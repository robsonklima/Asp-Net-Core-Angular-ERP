using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using NLog;
using SAT.MODELS.Views;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class IntegracaoBBService : IIntegracaoBBService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IOrdemServicoService _ordemServicoService;
        private ILocalAtendimentoService _localAtendimentoService;
        private IEquipamentoContratoService _equipamentoContratoService;
        private IIntegracaoBBRepository _integracaoBBRepo;
        private IConfiguration _configuration;

        public IntegracaoBBService(
            IOrdemServicoService ordemServicoService,
            ILocalAtendimentoService localAtendimentoService,
            IEquipamentoContratoService equipamentoContratoService,
            IIntegracaoBBRepository integracaoBBRepo,
            IConfiguration configuration
        )
        {
            _ordemServicoService = ordemServicoService;
            _localAtendimentoService = localAtendimentoService;
            _equipamentoContratoService = equipamentoContratoService;
            _integracaoBBRepo = integracaoBBRepo;
            _configuration = configuration;
        }

        public void Processar()
        {
            _logger.Info($"{MsgConst.INI_PROC} {Constants.INTEGRACAO_BB}");

            try
            {
                List<string> files = GenericHelper
                    .LerDiretorioInput("*crm549*")
                    .Where(f => f[0] == '1')
                    .ToList();

                if (files is null)
                {
                    _logger.Info(MsgConst.NENHUM_REGISTRO_ENCONTRADO);
                }
                else
                {
                    GenericHelper.MoverArquivosProcessados();

                    _logger.Info($"{MsgConst.ENCONTRADOS}: {files.Count()} registros");

                    List<OrdemServico> chamadosPerto = new();

                    files.ForEach((file) =>
                    {
                        string conteudoNormalizado = NormalizarConteudo(file);
                        OrdemServicoBB chamadoCliente = ExtrairChamadoArquivoAbertura(conteudoNormalizado);
                        OrdemServico chamadoPerto = AbrirChamadoPerto(chamadoCliente);
                        chamadosPerto.Add(chamadoPerto);
                    });

                    CriarArquivoRetornoAbertura(chamadosPerto);
                }

                CriarArquivoRetornoFechamento();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            _logger.Info($"{MsgConst.FIN_PROC} {Constants.INTEGRACAO_BB}");
        }

        private OrdemServicoBB ExtrairChamadoArquivoAbertura(string conteudo)
        {
            if (conteudo.Length < Constants.INT_BB_TAMANHO_ARQUIVO)
            {
                _logger.Info(MsgConst.MENOR_ESPECIFICADO + Constants.INT_BB_TAMANHO_ARQUIVO);

                return null;
            }

            OrdemServicoBB osCliente = new OrdemServicoBB
            {
                NumOSCliente = conteudo.Substring(1, 14).Trim(),
                TipoOS = conteudo.Substring(18, 3).Trim(),
                NumAgencia = String.Format("{0:00000}", conteudo.Substring(28, 4)).Trim(),
                DCPosto = String.Format("{0:00}", conteudo.Substring(32, 2)).Trim(),
                NomeAgencia = conteudo.Substring(34, 20).Trim(),
                HoraInicial = conteudo.Substring(54, 17).Trim(),
                HoraFinal = conteudo.Substring(54, 17).Trim(),
                DDDDependencia = conteudo.Substring(71, 3).Trim(),
                Telefone = conteudo.Substring(75, 10).Trim(),
                CGC = conteudo.Substring(84, 15).Trim(),
                Endereco = conteudo.Substring(99, 35).Trim(),
                Bairro = conteudo.Substring(134, 25).Trim(),
                Cidade = conteudo.Substring(159, 28).Trim(),
                UF = conteudo.Substring(187, 2).Trim(),
                CEP = conteudo.Substring(189, 8).Trim(),
                Criticidade = conteudo.Substring(198, 2).Trim(),
                DescCriticidade = conteudo.Substring(200, 50).Trim(),
                NumBem = conteudo.Substring(250, 13).Trim(),
                DescricaoBem = conteudo.Substring(263, 35).Trim(),
                NumSerie = conteudo.Substring(298, 17).Trim(),
                Modelo = conteudo.Substring(315, 10).Trim(),
                BilheteOS = conteudo.Substring(325, 13).Trim(),
                GarantiaBem = conteudo.Substring(338, 4).Trim(),
                Impacto = conteudo.Substring(342, 2).Trim(),
                DescricaoImpacto = conteudo.Substring(344, 10).Trim(),
                Defeito = conteudo.Substring(354, 129).Trim(),
                TipoManutencao = conteudo.Substring(484, 4).Trim(),
                DescricaoManutencao = conteudo.Substring(488, 20).Trim(),
                CodigoFornecedor = conteudo.Substring(508, 13).Trim(),
                DescricaoFornecedor = conteudo.Substring(521, 40).Trim(),
                MatriculaAberturaOS = conteudo.Substring(561, 8).Trim(),
                DataAberturaOS = conteudo.Substring(569, 8).Trim(),
                HoraAberturaOS = conteudo.Substring(577, 6).Trim(),
                NomeContato = conteudo.Substring(583, 20).Trim(),
                NumeroChamada = conteudo.Substring(643, 10).Trim(),
                DataChamadaOS = conteudo.Substring(653, 8).Trim(),
                HoraChamadaOS = conteudo.Substring(661, 6).Trim(),
                ChaveDoChamador = conteudo.Substring(667, 8).Trim(),
                NumeroDiasAtendimento = conteudo.Substring(675, 4).Trim(),
                NumeroHorasAtendimento = conteudo.Substring(679, 6).Trim(),
                NumeroContrato = conteudo.Substring(685, 12).Trim(),
                CodigoMantenedora = conteudo.Substring(697, 10).Trim(),
                IndOSGarantia = conteudo.Substring(707, 1).Trim(),
                DataAgendaOS = conteudo.Substring(708, 8).Trim(),
                HoraAgendaOS = conteudo.Substring(716, 6).Trim(),
                PrefixoDependenciaDestino = conteudo.Substring(722, 4).Trim(),
                SubordinadaDependenciaDestino = conteudo.Substring(726, 2).Trim(),
                CodigoTipoBem = conteudo.Substring(728, 2).Trim(),
                TextoMotivoManutencao = conteudo.Substring(730, 100).Trim()
            };

            return osCliente;
        }


        private void CriarArquivoRetornoAbertura(List<OrdemServico> chamados)
        {
            _logger.Info(MsgConst.INICIANDO_EXTR);

            try
            {
                string dataHora = DateTime.Now.ToString("ddMMyyyyHHMMsss");
                string fileName = $"CRM549R.xPerto01.{dataHora}.bco001";
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Output" + "/" + fileName;

                using (StreamWriter w = new StreamWriter(path))
                {
                    string cabecalho = MontarCabecalhoArquivoAbertura(chamados);
                    w.WriteLine(cabecalho);
                    _logger.Info(MsgConst.AD_CABECALHO);

                    chamados.ForEach(chamado =>
                    {
                        string linha = MontarLinhaArquivoAbertura(chamado);

                        w.WriteLine(linha);

                        _logger.Info(MsgConst.AD_LINHA);
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            _logger.Info(MsgConst.FIN_EXTR);
        }

        private string MontarLinhaArquivoAbertura(OrdemServico chamado)
        {
            _logger.Info(MsgConst.INI_LIN_FECH);

            string data = DateTime.Now.ToString("DDMMyyyy");
            string hora = DateTime.Now.ToString("HHMM");
            string retorno = $"3{chamado.NumOSCliente.PadLeft(14)} {data} {hora}{chamado.CodOS.ToString().PadLeft(10)}00";

            _logger.Info(MsgConst.FIN_LIN_FECH);

            return $"";
        }

        private string MontarCabecalhoArquivoAbertura(List<OrdemServico> chamados)
        {
            _logger.Info(MsgConst.INIC_CAB_ABERT);

            string horaAtual = DateTime.Now.ToString("HHmms");
            string dataGeracao = DateTime.Now.ToString("DDMMyyy");
            string horaGeracao = DateTime.Now.ToString("HHmms");
            string qtdChamados = String.Format("{0:00000}", chamados.Count());

            _logger.Info(MsgConst.FIN_CAB_ABERT);

            return $"2{dataGeracao} {horaGeracao}3{qtdChamados}crm549R       ";
        }


        private string MontarLinhaArquivoFechamento(ViewIntegracaoBB chamado)
        {
            _logger.Info(MsgConst.INIC_LIN_FECH);
            string numRAT = String.Format("{0:0000000000}", chamado.NumRAT);
            string horaGeracao = DateTime.Now.ToString("HHmms");

            string linha1 = @$"{chamado.NumOSCliente.PadLeft(14)}1{chamado.DataInicioAtendimento}{chamado.HoraInicioAtendimento}{chamado.DataFimAtendimento}{chamado.HoraFimAtendimento}{chamado.DataFinal}{chamado.HoraFinal}{numRAT}{chamado.NomeTecnico}000{chamado.DeParaCausa}0000{chamado.SituacaoOS}01{chamado.DataAgendamento}{chamado.HoraAgendamento}{horaGeracao}      00002{Environment.NewLine}";
            string linha2 = @$"{chamado.NumOSCliente.PadLeft(14)}2AAA007       00000000000000000000000000000                                                         00000      00003{Environment.NewLine}";
            string linha3 = @$"{chamado.NumOSCliente.PadLeft(14)}30090FECHAMENTO                                                                                     {horaGeracao}      00004";
            string retorno = linha1 + linha2 + linha3;

            _logger.Info(MsgConst.FIN_LIN_FECH);

            return retorno;
        }

        private string MontarCabecalhoArquivoFechamento()
        {
            _logger.Info(MsgConst.INIC_CAB_FECH);

            string data = DateTime.Now.ToString("DDMMyyyy");
            string hora = DateTime.Now.ToString("HHMMs");

            string retorno = @$"000000000000000{hora}CRM558A400306444                                                                                 {data}00001";

            _logger.Info($"{MsgConst.FIN_CAB_FECH}");

            return retorno;
        }

        private string MontarRodapeArquivoFechamento()
        {
            _logger.Info(MsgConst.INI_RODAPE_FECH);

            string hora = DateTime.Now.ToString("HHMMs");

            string retorno = @$"999999999999999{hora}                                                                                          00000000000000400000";

            _logger.Info(MsgConst.FIN_RODAPE_FECH);

            return retorno;
        }

        private void CriarArquivoRetornoFechamento()
        {
            _logger.Info(MsgConst.INI_ARQ_RET);

            try
            {
                var parameters = new IntegracaoBBParameters { };
                var chamados = _integracaoBBRepo.ObterPorParametros(parameters);

                foreach (var chamado in chamados)
                {
                    string dataHora = DateTime.Now.ToString("ddMMyyyyHHMMsss");
                    string fileName = "crm558a.xperto01." + dataHora + ".bco001";
                    string path = System.AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Output" + "/" + fileName;

                    using (StreamWriter w = new StreamWriter(path))
                    {
                        string cabecalho = MontarCabecalhoArquivoFechamento();
                        string rodape = MontarRodapeArquivoFechamento();

                        w.WriteLine(cabecalho);

                        chamados.ForEach(chamado =>
                        {
                            string linha = MontarLinhaArquivoFechamento(chamado);

                            w.WriteLine(linha);
                        });

                        w.WriteLine(rodape);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            _logger.Info(MsgConst.FIN_ARQ_RET);
        }

        private string NormalizarConteudo(string conteudo)
        {
            Regex reg = new Regex("[*'\",_&#^@]");

            return reg.Replace(conteudo, " ");
        }

        private OrdemServico AbrirChamadoPerto(OrdemServicoBB chamadoCliente)
        {
            EquipamentoContrato equipamentoContrato = (EquipamentoContrato)_equipamentoContratoService
                .ObterPorParametros(new EquipamentoContratoParameters
                {
                    NumSerie = chamadoCliente.NumSerie,
                    CodClientes = Constants.CLIENTE_BB.ToString()
                })
                .Items.FirstOrDefault();

            if (equipamentoContrato is null)
            {
                _logger.Info(MsgConst.EQUIP_N_ENCONTR);

                return null;
            }

            LocalAtendimento localAtendimento = (LocalAtendimento)_localAtendimentoService
                .ObterPorParametros(new LocalAtendimentoParameters
                {
                    NumAgencia = chamadoCliente.NumAgencia,
                    DCPosto = chamadoCliente.DCPosto,
                    CodCliente = Constants.CLIENTE_BB
                })
                .Items.FirstOrDefault();

            var osJaAberta = (OrdemServico)_ordemServicoService.ObterPorParametros(new OrdemServicoParameters
            {
                CodCliente = Constants.CLIENTE_BB,
                NumOSCliente = chamadoCliente.NumOSCliente
            }).Items.FirstOrDefault();

            if (osJaAberta is not null)
            {
                _logger.Info(MsgConst.ORDEM_JA_ABERTA);

                return osJaAberta;
            }

            if (localAtendimento is null)
                _logger.Info(MsgConst.LOCAL_N_ENCONTR);

            var observacaoCliente = $@"
                    OS INTEGRADA DIA {DateTime.Now.ToString()}
                    SÉRIE: {equipamentoContrato.NumSerie}
                    AGÊNCIA: {localAtendimento.NumAgencia}/{localAtendimento.DCPosto} - {localAtendimento.NomeLocal}
                    ENDEREÇO: {chamadoCliente.Endereco}
                    BAIRRO: {chamadoCliente.Bairro}
                    UF: {chamadoCliente.UF}
                    CIDADE: {chamadoCliente.Cidade}
                    CEP: {chamadoCliente.CEP}
                ";

            var ordemServico = new OrdemServico
            {
                CodCliente = Constants.CLIENTE_BB,
                CodPosto = (int)localAtendimento.CodPosto,
                CodEquipContrato = equipamentoContrato.CodEquipContrato,
                NomeContato = chamadoCliente.NomeContato,
                DefeitoRelatado = chamadoCliente.Defeito,
                TelefoneSolicitante = chamadoCliente.Telefone,
                NomeSolicitante = chamadoCliente.NomeContato,
                ObservacaoCliente = observacaoCliente,
                IndServico = 1,
                IndIntegracao = 1,
                DataHoraCad = DateTime.Now,
                DataHoraAberturaOS = DateTime.Now,
                CodUsuarioCad = "SAT",
                CodStatusServico = Constants.STATUS_SERVICO_ABERTO,
                CodTipoIntervencao = Constants.CORRETIVA,
                CodEquip = equipamentoContrato.CodEquip,
                CodRegiao = equipamentoContrato.CodRegiao,
                CodAutorizada = equipamentoContrato.CodAutorizada,
                CodFilial = equipamentoContrato.CodFilial
            };

            OrdemServico novaOrdemServico = _ordemServicoService.Criar(ordemServico);

            _logger.Info(MsgConst.OS_CRIADA);

            return novaOrdemServico;
        }
    }
}
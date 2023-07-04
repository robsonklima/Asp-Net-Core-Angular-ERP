using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Fluent;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class IntegracaoBanrisulService : IIntegracaoBanrisulService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEmailService _emailService;
        private readonly IOrdemServicoService _ordemServicoService;
        private readonly IRelatorioAtendimentoService _relatorioAtendimentoService;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;
        private readonly ILocalAtendimentoService _localAtendimentoService;
        private readonly IArquivoBanrisulService _arquivoBanrisulService;
        private readonly IExportacaoService _exportacaoService;

        public IntegracaoBanrisulService(
            IEmailService emailService,
            IOrdemServicoService ordemServicoService,
            IRelatorioAtendimentoService relatorioAtendimentoService,
            IEquipamentoContratoRepository equipamentoContratoRepo,
            ILocalAtendimentoService localAtendimentoService,
            IArquivoBanrisulService arquivoBanrisulService,
            IExportacaoService exportacaoService
        )
        {
            _emailService = emailService;
            _ordemServicoService = ordemServicoService;
            _relatorioAtendimentoService = relatorioAtendimentoService;
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _localAtendimentoService = localAtendimentoService;
            _arquivoBanrisulService = arquivoBanrisulService;
            _exportacaoService = exportacaoService;
        }

        public async Task ProcessarEmailsAsync()
        {
            var emails = await _emailService.ObterEmailsAsync(Constants.EMAIL_BANRISUL_CONFIG.ClientID);

            _logger.Info()
                .Message("{ qtd } e-mails encontrados para processamento", emails.Value.Count)
                .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                .Write();

            foreach (var email in emails.Value)
            {
                _logger.Info()
                    .Message("Processando e-mail")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                await _emailService.DeletarEmailAsync(Constants.EMAIL_BANRISUL_CONFIG.ClientID, email.Id);

                var atendimento = Carrega(email.Body.Content);

                TentaCadastro(atendimento);
            }
        }

        public void ProcessarRetornos()
        {
            _logger.Info()
                .Message("iniciando processamento de retorno em pdf")
                .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                .Write();

            var arquivosPendentes = _arquivoBanrisulService
                .ObterPorParametros(new ArquivoBanrisulParameters {
                    IndPDFGerado = 0,
                    PageSize = 10
                });

            _logger.Info()
                .Message("{ pendencias } arquivos encontrados para processamento", arquivosPendentes.Items.Count())
                .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                .Write();

            foreach (ArquivoBanrisul arquivo in arquivosPendentes.Items)
            {
                try
                {
                    var parametros = new OrdemServicoParameters {
                        CodOS = arquivo.CodOS.ToString()
                    };

                    arquivo.TextoEmail = arquivo.TextoEmail.Replace("|", "<br />");

                    _logger.Info()
                        .Message("Processando arquivo do chamado {os}", arquivo.CodOS)
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Write();

                    var email = new Email {
                        EmailDestinatarios = new string[] { Constants.BANRISUL_EMAIL, Constants.BANRISUL_EMAIL2, Constants.EQUIPE_SAT_EMAIL },
                        Assunto = arquivo.AssuntoEmail,
                        Corpo = arquivo.TextoEmail                    
                    };

                    _exportacaoService.Exportar(new Exportacao {
                        FormatoArquivo = ExportacaoFormatoEnum.PDF,
                        TipoArquivo = ExportacaoTipoEnum.ORDEM_SERVICO,
                        EntityParameters = JObject.FromObject(parametros),
                        Email = email
                    });

                    arquivo.IndPDFGerado = 1;
                    _arquivoBanrisulService.Atualizar(arquivo);

                    _logger.Info()
                        .Message("Enviado retorno em pdf da OS {os}", arquivo.CodOS)
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Write();
                }
                catch (Exception ex)
                {
                    _logger.Error()
                        .Message("Erro ao enviar retorno em pdf")
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Exception(ex)
                        .Write();
                }
            }
        }

        private IntegracaoBanrisulAtendimento Carrega(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                _logger.Error()
                    .Message("Nao e possivel carregar o conteudo pois esta vazio")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();
            }

            conteudo = StringHelper.GetStringBetweenCharacters(conteudo, '#', '#');

            conteudo = conteudo.Replace("||", "|");

            string[] dados = conteudo.Split('|');

            int quantidadeCampos = dados.Length == 17 ? 17 : 16;

            if (dados.Length != quantidadeCampos)
            {
                _logger.Error()
                    .Message("A quantidade de campos encontrados e diferente do permitido")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return null;
            }

            IntegracaoBanrisulAtendimento atendimento = new();

            atendimento.NumeroIncidente.Valor = dados[atendimento.NumeroIncidente.Indice].ToString();
            atendimento.DataHoraAbertura.Valor = DateTime.Parse(dados[atendimento.DataHoraAbertura.Indice].ToString(), new CultureInfo("en-US")).ToString("dd/MM/yyyy HH:mm:ss");
            atendimento.NumeroSerie.Valor = dados[atendimento.NumeroSerie.Indice].ToString();
            atendimento.CodigoLocalEquipamento.Valor = dados[atendimento.CodigoLocalEquipamento.Indice].ToString().PadLeft(5, '0');
            atendimento.OrganizacaoFisicaEquipamento.Valor = dados[atendimento.OrganizacaoFisicaEquipamento.Indice].ToString();
            atendimento.NomeContato.Valor = dados[atendimento.NomeContato.Indice].ToString();
            atendimento.TelefoneContato.Valor = dados[atendimento.TelefoneContato.Indice].ToString();
            atendimento.HorarioAtendimento.Valor = dados[atendimento.HorarioAtendimento.Indice].ToString();
            atendimento.ClassificacaoAtendimento.Valor = dados[atendimento.ClassificacaoAtendimento.Indice].ToString();
            atendimento.DescricaoIncidente.Valor = dados[atendimento.DescricaoIncidente.Indice].ToString();
            atendimento.Rua.Valor = dados[atendimento.Rua.Indice].ToString();
            atendimento.Cidade.Valor = dados[atendimento.Cidade.Indice].ToString();
            atendimento.Cep.Valor = dados[atendimento.Cep.Indice].ToString();
            atendimento.StatusIncidente.Valor = dados[atendimento.StatusIncidente.Indice].ToString();
            atendimento.PrioridadeIncidente.Valor = dados[atendimento.PrioridadeIncidente.Indice].ToString();
            atendimento.DataHoraAgendamento.Valor = dados[atendimento.DataHoraAgendamento.Indice].ToString();
            atendimento.Conteudo = conteudo;

            if (atendimento.StatusIncidente.Valor.ToUpper().Equals("RE"))
            {
                string dataHoraSolucao =
                   !string.IsNullOrWhiteSpace(dados[atendimento.DataHoraSolucaoValida.Indice].ToString())
                    ? dados[atendimento.DataHoraSolucaoValida.Indice].ToString()
                    : dados[atendimento.DataHoraSolucaoValida.Indice - 1].ToString();

                atendimento.DataHoraSolucaoValida.Valor = DateTime.Parse(dataHoraSolucao, new CultureInfo("en-US")).ToString("dd/MM/yyyy HH:mm:ss");
            }

            return atendimento;
        }

        private void TentaCadastro(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                if (atendimento.StatusIncidente.Valor.ToUpper().Equals("ATENDIMENTO EM ORÇAMENTO"))
                {
                    AprovarOrcamento(atendimento);
                }
                else if (atendimento.StatusIncidente.Valor.ToUpper().Equals("REOP"))
                {
                    ReabrirOS(atendimento);
                }
                else if (atendimento.StatusIncidente.Valor.ToUpper().Equals("RE"))
                {
                    ResponderOS(atendimento);
                }
                else
                {
                    AbrirOS(atendimento);
                }
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message($"{ex.Message} {ex.InnerException}")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Exception(ex)
                    .Write();
            }
        }

        private void AprovarOrcamento(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                _logger.Info()
                    .Message("Iniciando aprovação de orçamento da OS cliente")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                OrdemServico ordemServico = ObterOrdemServico(atendimento.NumeroIncidente.Valor);
                string emailFilial = ordemServico.Filial.Email;

                if (ordemServico == null)
                {
                    EnviaEmailRecusaAbertura(atendimento, "Integração Banrisul ATM: Chamado de aprovação de orçamento encaminhado pelo cliente, porem chamado nao está cadastrado no sistema Perto.", emailFilial);

                    _logger.Error()
                        .Message("Chamado de aprovação de orçamento encaminhado pelo cliente, porem chamado nao está cadastrado no sistema Perto")
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Write();
                }

                ordemServico.CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO;
                ordemServico.DataHoraManut = DateTime.Now;
                ordemServico.CodUsuarioManutencao = "INTEGRACAO";

                ordemServico = _ordemServicoService.Atualizar(ordemServico);
                EnviaEmailAprovacaoOrcamento(atendimento, ordemServico.CodOS);
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message($"{ex.Message} {ex.InnerException}")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Exception(ex)
                    .Write();
            }
        }

        private void ReabrirOS(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                _logger.Info()
                    .Message("Iniciando reabertura da OS cliente")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                var ordemServico = ObterOrdemServico(atendimento.NumeroIncidente.Valor);

                string emailFilial = ordemServico.Filial.Email;

                if (ordemServico != null)
                {
                    EquipamentoContrato equipContrato = ordemServico.EquipamentoContrato;

                    if (equipContrato != null)
                    {
                        AcordoNivelServico sla = equipContrato.AcordoNivelServico;

                        if (sla != null)
                        {
                            DateTime dataSolucao = ordemServico.RelatoriosAtendimento
                                .Where(r => r.CodStatusServico == (int)StatusServicoEnum.FECHADO)
                                .OrderByDescending(r => r.DataHoraSolucao)
                                .FirstOrDefault()
                                .DataHoraSolucao;

                            DateTime hoje = DateTime.Now;
                            bool sabado = (bool)sla.IndSabado;
                            bool domingo = (bool)sla.IndDomingo;
                            bool feriado = (bool)sla.IndFeriado;
                            int codUF = 1;
                            int codCidade = 1;

                            LocalAtendimento localAtendimento = ordemServico.LocalAtendimento;
                            
                            if (localAtendimento != null)
                            {
                                Cidade cidade = ordemServico.LocalAtendimento.Cidade;

                                codCidade = localAtendimento.CodCidade;
                                codUF = cidade != null ? cidade.CodUF : 1;                                
                            }

                            double? horasNaoUteis = CalculaHorasNaoUteis(dataSolucao, DateTime.Now);
                            double horasUteis = (hoje - dataSolucao).TotalHours - horasNaoUteis.Value;

                            if (horasUteis <= 48)
                            {
                                ordemServico.CodStatusServico = 1;
                                ordemServico.NumOSQuarteirizada = "REABERTO";

                                _ordemServicoService.Atualizar(ordemServico);

                                EnviaEmailAbertura(atendimento, ordemServico, reaberto: true);

                                return;
                            }
                            else
                            {
                                EnviaEmailRecusaAbertura(atendimento, "Prazo de 48 horas já encerrado. Status do incidente: REOP.", emailFilial);

                                _logger.Error()
                                    .Message("Prazo de 48 horas já encerrado. Status do incidente: REOP")
                                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                                    .Write();
                            }
                        }
                        else
                        {
                            EnviaEmailRecusaAbertura(atendimento, "SLA nao encontrado: " + equipContrato.CodSLA, emailFilial);

                            _logger.Error()
                                .Message("SLA nao encontrada")
                                .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                                .Write();
                        }
                    }
                    else
                    {
                        EnviaEmailRecusaAbertura(atendimento, "Chamado nao encontrado. Status do incidente: REOP:" + ordemServico.CodContrato, emailFilial);

                        _logger.Error()
                            .Message("Contrato nao encontrado")
                            .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                            .Write();
                    }

                }
                else
                {
                    EnviaEmailRecusaAbertura(atendimento, "Chamado nao encontrado. Status do incidente: REOP.", emailFilial);

                    _logger.Error()
                        .Message("Chamado nao encontrado.Status do incidente: REOP.")
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Write();
                }
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message("Erro ao abrir chamado")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Exception(ex)
                    .Write();
            }
        }

        private void AbrirOS(IntegracaoBanrisulAtendimento atendimento)
        {
            _logger.Info()
                .Message("Iniciando abertura da OS cliente")
                .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                .Write();

            OrdemServico ordemServico = new();

            if (string.IsNullOrWhiteSpace(atendimento.DataHoraAbertura.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "A data de abertura e invalida ou nao foi informada.", null);

                _logger.Error()
                    .Message("A data de abertura e invalida ou nao foi informada")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (atendimento.StatusIncidente.Valor.ToUpper().Equals("CAC"))
            {
                EnviaEmailRecusaAbertura(atendimento, "Chamado de cancelamento ignorado. Status do incidente: CAC.", null);

                _logger.Error()
                    .Message("Chamado de cancelamento ignorado. Status do incidente: CAC")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (string.IsNullOrWhiteSpace(atendimento.NumeroSerie.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Nao foi possivel encontrar o numero de serie do equipamento do cliente no e-mail de atendimento.", null);

                _logger.Error()
                    .Message("Nao foi possivel encontrar o numero de serie do equipamento do cliente no e-mail de atendimento.")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            EquipamentoContrato equipamento = new();

            var equipamentos = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters
            {
                IndAtivo = 1,
                NumSerie = atendimento.NumeroSerie.Valor.Trim().ToUpper(),
                CodClientes = Constants.CLIENTE_BANRISUL.ToString()
            });

            if (equipamentos.Count() == 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "O numero de serie do equipamento do cliente encontrado no e-mail nao está cadastrado no sistema.", null);

                _logger.Error()
                    .Message("O numero de serie do equipamento do cliente encontrado no e-mail nao está cadastrado no sistema.")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (equipamentos.Count() > 1)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontrados mais de um equipamento no sistema com o mesmo numero de serie e local de atendimento.", null);
                
                _logger.Error()
                    .Message("Foram encontrados mais de um equipamento no sistema com o mesmo numero de serie e local de atendimento")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (equipamentos.Count() == 1)
            {
                equipamento = (EquipamentoContrato)equipamentos.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(atendimento.CodigoLocalEquipamento.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Nao foi possivel encontrar a agencia no e-mail de atendimento.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("Nao foi possivel encontrar a agencia no e-mail de atendimento")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (atendimento.CodigoLocalEquipamento.Valor.Length < 5)
            {
                EnviaEmailRecusaAbertura(atendimento, "O codigo da agencia informado no e-mail deve conter 5 posições.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("O codigo da agencia informado no e-mail deve conter 5 posições")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            LocalAtendimento local = new();

            var locais = _localAtendimentoService.ObterPorParametros(new LocalAtendimentoParameters
            {
                CodClientes = Constants.CLIENTE_BANRISUL.ToString(),
                CodPosto = equipamento.CodPosto,
                IndAtivo = 1
            }).Items;

            if (locais.Count() == 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "A agencia encontrada no e-mail nao está cadastrada no sistema.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("A agencia encontrada no e-mail nao está cadastrada no sistema")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (locais.Count() > 1)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontradas mais de uma agencia no sistema com o mesmo codigo de agencia e posto.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("Foram encontradas mais de uma agencia no sistema com o mesmo codigo de agencia e posto")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            if (locais.Count() == 1)
            {
                local = (LocalAtendimento)locais.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(atendimento.NumeroIncidente.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Nao foi possivel encontrar o numero do chamado do cliente no e-mail de atendimento.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("Nao foi possivel encontrar o numero do chamado do cliente no e-mail de atendimento")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;                    
            }

            var p = new OrdemServicoParameters
            {
                NumOSCliente = atendimento.NumeroIncidente.Valor,
                CodClientes = Constants.CLIENTE_BANRISUL.ToString(),
                NotIn_CodStatusServicos = ((int)StatusServicoEnum.CANCELADO).ToString(),
                SortActive = "CodOS",
                SortDirection = "DESC"
            };

            var chamados = _ordemServicoService.ObterPorParametros(p).Items;

            if (chamados.Count() > 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "Ja existe um chamado do cliente cadastrado no sistema com este mesmo numero de chamado.", equipamento.Filial.Email);
                
                _logger.Error()
                    .Message("Ja existe um chamado do cliente cadastrado no sistema com este mesmo numero de chamado")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                return;
            }

            if (!string.IsNullOrWhiteSpace(atendimento.DataHoraAgendamento.Valor))
            {
                ordemServico.ObservacaoCliente += Environment.NewLine +
                    String.Format("Agendamento: {0}", DateTime.Parse(atendimento.DataHoraAgendamento.Valor, new CultureInfo("en-US")).ToString("dd/MM/yyyy HH:mm:ss"));
            }

            if (atendimento.DescricaoIncidente.Valor.Contains("RECALL DOS SENSORES FIM DE CeDULAS DO DISPENSADOR"))
            {
                ordemServico.CodTipoIntervencao = (int)TipoIntervencaoEnum.ALTERACAO_DE_ENGENHARIA;
                ordemServico.NumOSQuarteirizada = "REC FIM DE CED";
            }
            else
            {
                ordemServico.CodTipoIntervencao = GeraTipoIntervencao(atendimento.ClassificacaoAtendimento.Valor.ToUpper());
            }

            ordemServico.CodPosto = local.CodPosto > 0 ? (int)local.CodPosto : (int)ordemServico.EquipamentoContrato.CodPosto;
            ordemServico.CodAutorizada = equipamento.CodAutorizada;
            ordemServico.NumOSCliente = atendimento.NumeroIncidente.Valor;
            ordemServico.CodEquip = (int)equipamento.CodEquip;
            ordemServico.CodEquipContrato = equipamento.CodEquipContrato;
            ordemServico.CodFilial = equipamento.CodFilial;
            ordemServico.CodGrupoEquip = equipamento.CodGrupoEquip;
            ordemServico.CodRegiao = equipamento.CodRegiao;
            ordemServico.CodTipoEquip = equipamento.CodTipoEquip;
            ordemServico.NomeSolicitante = atendimento.NomeContato.Valor;
            ordemServico.IndServico = 1;
            ordemServico.IndIntegracao = 1;
            ordemServico.DataHoraAberturaOS = DateTime.Now;
            ordemServico.DataHoraCad = DateTime.Now;
            ordemServico.CodUsuarioCad = "INTEGRACAO";
            ordemServico.DefeitoRelatado = atendimento.DescricaoIncidente.Valor;
            ordemServico.ObservacaoCliente = String.Format("Horário de Atendimento: {0}", atendimento.HorarioAtendimento.Valor);
            ordemServico.CodCliente = (int)Constants.CLIENTE_BANRISUL;
            ordemServico.CodStatusServico = (int)StatusServicoEnum.ABERTO;
            ordemServico.DataHoraSolicitacao = DateTime.Parse(atendimento.DataHoraAbertura.Valor);
            ordemServico.IndStatusEnvioReincidencia = 0;
            ordemServico.IndRevisaoReincidencia = 0;

            ordemServico = _ordemServicoService.Criar(ordemServico);
            EnviaEmailAbertura(atendimento, ordemServico);
        }

        private void ResponderOS(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                _logger.Info()
                    .Message("Iniciando retorno de status da OS cliente {cliente}", atendimento.NumeroIncidente.Valor)
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Write();

                var ordemServico = ObterOrdemServico(atendimento.NumeroIncidente.Valor);

                if (ordemServico.RelatoriosAtendimento.Count > 0)
                {
                    RelatorioAtendimento ultimaRAT = ordemServico.RelatoriosAtendimento.OrderByDescending(q => q.CodRAT).FirstOrDefault();
                    DateTime parseDate = new DateTime();

                    if (DateTime.TryParse(atendimento.DataHoraSolucaoValida.Valor, out parseDate))
                    {
                        ultimaRAT.DataHoraSolucaoValida = parseDate;
                        _relatorioAtendimentoService.Atualizar(ultimaRAT);

                        EnviaEmailResolucao(atendimento, "Resolução acatada com sucesso", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RECEBIDA);
                        return;
                    }
                    else
                    {
                        EnviaEmailResolucao(atendimento, "DataHoraSolucaoValida invalida. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_DATA_INVALIDA);

                        _logger.Error()
                            .Message("DataHoraSolucaoValida invalida. Status do incidente: RE.")
                            .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                            .Write();
                    }
                }
                else
                {
                    EnviaEmailResolucao(atendimento, "RAT nao encontrada. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA);

                    _logger.Error()
                        .Message("RAT nao encontrada")
                        .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                        .Write();
                }
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message($"{ex.Message} {ex.InnerException}")
                    .Property("application", Constants.INTEGRACAO_BANRISUL_ATM)
                    .Exception(ex)
                    .Write();
            }
        }

        private OrdemServico ObterOrdemServico(string numOSCliente)
        {
            return (OrdemServico)_ordemServicoService
                .ObterPorParametros(new OrdemServicoParameters
                {
                    NumOSCliente = numOSCliente,
                    CodClientes = Constants.CLIENTE_BANRISUL.ToString()
                }).Items.FirstOrDefault();
        }

        private string GetHtmlEmailAbertura(IntegracaoBanrisulAtendimento atendimento, String mensagem)
        {
            StringBuilder texto = new StringBuilder();

            texto.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >");
            texto.Append("<html>");
            texto.Append("	<body>");
            texto.Append("			<font style=\"FONT-SIZE: 12pt\">");
            texto.Append("				<div align=\"center\">");
            texto.AppendFormat("            {0}", mensagem);
            texto.Append("				</div>");
            texto.Append("			</font>");
            texto.Append("			<br>");
            texto.Append("			<table style=\"BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid ;FONT-SIZE: 10pt; FONT-FAMILY: verdana\" align=\"center\">");
            texto.Append("				<tr>");
            texto.Append("					<td style=\"BACKGROUND-COLOR: #47699b; COLOR: white\" align=\"center\" colspan=\"2\">");
            texto.Append("						<font style=\"FONT-WEIGHT: bold\">");
            texto.Append("							Dados do chamado");
            texto.Append("						</font>");
            texto.Append("					</td>");
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">numero Chamado:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroIncidente.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">agencia:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Posto:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">numero Serie Equipamento:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroSerie.Valor);
            texto.Append("				</tr>");
            texto.Append("			</table>");
            texto.Append("	</body>");
            texto.Append("</html>");

            return texto.ToString();
        }

        private string GetHtmlEmailResolucao(IntegracaoBanrisulAtendimento atendimento, String mensagem)
        {
            StringBuilder texto = new StringBuilder();

            texto.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >");
            texto.Append("<html>");
            texto.Append("	<body>");
            texto.Append("			<font style=\"FONT-SIZE: 12pt\">");
            texto.Append("				<div align=\"center\">");
            texto.AppendFormat("            {0}", mensagem);
            texto.Append("				</div>");
            texto.Append("			</font>");
            texto.Append("			<br>");
            texto.Append("			<table style=\"BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid ;FONT-SIZE: 10pt; FONT-FAMILY: verdana\" align=\"center\">");
            texto.Append("				<tr>");
            texto.Append("					<td style=\"BACKGROUND-COLOR: #47699b; COLOR: white\" align=\"center\" colspan=\"2\">");
            texto.Append("						<font style=\"FONT-WEIGHT: bold\">");
            texto.Append("							Dados do chamado");
            texto.Append("						</font>");
            texto.Append("					</td>");
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">numero Chamado:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroIncidente.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">agencia:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Posto:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">numero Serie Equipamento:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroSerie.Valor);
            texto.Append("				</tr>");
            texto.Append("			</table>");
            texto.Append("	</body>");
            texto.Append("</html>");

            return texto.ToString();
        }

        private void EnviaEmailAbertura(IntegracaoBanrisulAtendimento atendimento, OrdemServico ordemServico, bool reaberto = false)
        {
            string assunto = "Integração Banrisul ATM - Situação chamado cliente " +
                atendimento.NumeroIncidente.Valor + " - Analista " + atendimento.NomeContato.Valor;

            string status = reaberto ? "reaberto" : "cadastrado";
            string mensagem = "Chamado cliente " + atendimento.NumeroIncidente.Valor +
                " " + status + " com sucesso.<br>Numero do chamado cadastrado no SAT " + ordemServico.CodOS + "<br>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            string[] destinatarios = { "equipe.sat@perto.com.br", "giane.santos@perto.com.br" };

            _emailService.Enviar(new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = assunto,
                Corpo = texto
            });
        }

        private void EnviaEmailRecusaAbertura(IntegracaoBanrisulAtendimento atendimento, String motivo, string destinatario = "")
        {
            string assunto = "Integração Banrisul ATM - Situação chamado cliente " +
                atendimento.NumeroIncidente.Valor + " - Analista " + atendimento.NomeContato.Valor;

            string mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " recusado pois nao atendeu aos seguintes requisitos: " + "<br/><ul><li>" + motivo + "</li></ul>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            string[] destinatarios = { "equipe.sat@perto.com.br", "giane.santos@perto.com.br" };

            _emailService.Enviar(new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = assunto,
                Corpo = texto
            });
        }

        private void EnviaEmailResolucao(IntegracaoBanrisulAtendimento atendimento, String motivo, string destinatario = "",
            IntegracaoBanrisulResolucaoEnum IntegracaoBanrisulResolucaoEnum = IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RECEBIDA)
        {
            string assunto = "Integração Banrisul ATM - Resolução do atendimento " + atendimento.NumeroIncidente.Valor;

            string mensagem = null;
            switch (IntegracaoBanrisulResolucaoEnum)
            {
                case IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RECEBIDA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução acatada com sucesso.";
                    break;
                case IntegracaoBanrisulResolucaoEnum.RESOLUCAO_DATA_INVALIDA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução nao acatada, Data de resolução invalida.";
                    break;
                case IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução nao acatada, RAT do chamado nao encontrada.";
                    break;
            }

            string texto = GetHtmlEmailResolucao(atendimento, mensagem);

            string[] destinatarios = { "equipe.sat@perto.com.br", "giane.santos@perto.com.br", destinatario };

            _emailService.Enviar(new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = assunto,
                Corpo = texto
            });
        }

        private void EnviaEmailAprovacaoOrcamento(IntegracaoBanrisulAtendimento atendimento, int CodOS)
        {
            string assunto = "Integração Banrisul - Aprovação Orçamento - OS: " + CodOS + ".";

            string mensagem = "Chamado Cliente: " + atendimento.NumeroIncidente.Valor + " com orçamento aprovado de acordo com notificação via integração. " + "<br/> <h5 style='color: steelblue;'>numero OS Perto: 12365478</h5><br/>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            string[] destinatarios = { "equipe.sat@perto.com.br", "giane.santos@perto.com.br", "rosimar.silva@perto.com.br", "dss.orcamentos@perto.com.br" };

            _emailService.Enviar(new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = assunto,
                Corpo = texto
            });
        }

        private int GeraTipoIntervencao(string ClassificacaoAtendimento)
        {
            if ("TI.EQUIPAMENTO.CASH.DESINSTALAÇÃO".Equals(ClassificacaoAtendimento) ||
                "TI.EQUIPAMENTO.TALONADORA.DESINSTALAÇÃO".Equals(ClassificacaoAtendimento) ||
                "TI.EQUIPAMENTO.TC.DESINSTALAÇÃO".Equals(ClassificacaoAtendimento))
                return 3;
            else if ("TI.EQUIPAMENTO.CASH.MANUTENÇÃO PREVENTIVA".Equals(ClassificacaoAtendimento))
                return 6;
            else if ("TI.EQUIPAMENTO.CASH.REINSTALAÇÃO".Equals(ClassificacaoAtendimento) ||
                "TI.EQUIPAMENTO.TALONADORA.REINSTALAÇÃO".Equals(ClassificacaoAtendimento) ||
                "TI.EQUIPAMENTO.TC.REINSTALAÇÃO".Equals(ClassificacaoAtendimento))
                return 7;
            else if ("TI.EQUIPAMENTO.CASH.INSTALAÇÃO DO KIT BOCAL E PLACA CDP NO ATM".Equals(ClassificacaoAtendimento) || 
                    "TI.EQUIPAMENTO.CASH.INSTALAÇÃO DO KIT DE ENTINTAMENTO DE ATM".Equals(ClassificacaoAtendimento) ||
                    "TI.EQUIPAMENTO.CASH.RECALL PLACA CDP".Equals(ClassificacaoAtendimento) || 
                    "TI.EQUIPAMENTO.CASH.RECALL TAMPA DE K7".Equals(ClassificacaoAtendimento))
                return 1;
            else
                return 2;                
        }

        private double? CalculaHorasNaoUteis(DateTime inicio, DateTime fim)
        {
            List<DateTime> datas = new List<DateTime>();

            while (inicio.Date <= fim.Date)
            {
                if (fim.DayOfWeek == DayOfWeek.Sunday || fim.DayOfWeek == DayOfWeek.Saturday)
                {
                    datas.Insert(0, fim);
                }

                fim = fim.AddDays(-1);
            }

            return datas.Count * 24;
        }
    }
}
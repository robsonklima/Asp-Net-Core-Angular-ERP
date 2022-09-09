using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
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
        private IEmailService _emailService;
        private IOrdemServicoService _ordemServicoService;
        private IRelatorioAtendimentoService _relatorioAtendimentoService;
        private IEquipamentoContratoRepository _equipamentoContratoRepo;
        private ILocalAtendimentoService _localAtendimentoService;
        private IFeriadoService _feriadoService;
        private readonly IArquivoBanrisulService _arquivoBanrisulService;

        public IntegracaoBanrisulService(
            IEmailService emailService,
            IOrdemServicoService ordemServicoService,
            IRelatorioAtendimentoService relatorioAtendimentoService,
            IEquipamentoContratoRepository equipamentoContratoRepo,
            ILocalAtendimentoService localAtendimentoService,
            IFeriadoService feriadoService,
            IArquivoBanrisulService arquivoBanrisulService
        )
        {
            _emailService = emailService;
            _ordemServicoService = ordemServicoService;
            _relatorioAtendimentoService = relatorioAtendimentoService;
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _localAtendimentoService = localAtendimentoService;
            _feriadoService = feriadoService;
            _arquivoBanrisulService = arquivoBanrisulService;
        }

        public async Task ExecutarAsync()
        {
            ProcessarArquivos();

            await ProcessarEmailsAsync();
        }

        private async Task ProcessarEmailsAsync()
        {
            var emails = await _emailService.ObterEmailsAsync(Constants.EMAIL_TESTE_CONFIG.ClientID);

            foreach (var email in emails.Value)
            {
                //await _emailService.DeletarEmailAsync(Constants.EMAIL_TESTE_CONFIG.ClientID, email.Id);

                var atendimento = Carrega(email.Body.Content);

                TentaCadastro(atendimento);
            }
        }

        private void ProcessarArquivos() 
        {
            var arquivosPendentes = _arquivoBanrisulService
                .ObterPorParametros(new ArquivoBanrisulParameters {
                    IndPDFGerado = 0
                });

                        
        }

        private IntegracaoBanrisulAtendimento Carrega(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                RegistrarExcecao($"Integração Banrisul ATM: Não é possivel carregar o conteudo pois está vazio. Conteudo encontrado: {conteudo}");
            }

            conteudo = StringHelper.GetStringBetweenCharacters(conteudo, '#', '#');

            conteudo = conteudo.Replace("||", "|");

            string[] dados = conteudo.Split('|');

            int quantidadeCampos = dados.Length == 17 ? 17 : 16;

            if (dados.Length != quantidadeCampos)
            {
                RegistrarExcecao($"Integração Banrisul ATM: A quantidade de campos encontrados é diferente do permitido. Conteudo encontrado: {conteudo}");
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
                RegistrarExcecao($"Integração Banrisul ATM: {atendimento.Conteudo}, erro: {ex.Message}");
            }
        }

        private void AprovarOrcamento(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                OrdemServico ordemServico = ObterOrdemServico(atendimento.NumeroIncidente.Valor);
                string emailFilial = ordemServico.Filial.Email;

                if (ordemServico == null)
                {
                    EnviaEmailRecusaAbertura(atendimento, "Integração Banrisul ATM: Chamado de aprovação de orçamento encaminhado pelo cliente, porém chamado não está cadastrado no sistema Perto.", emailFilial);

                    RegistrarExcecao($"Integração Banrisul ATM: Chamado de aprovação de orçamento encaminhado pelo cliente, porém chamado não está cadastrado no sistema Perto. {atendimento.Conteudo}");
                }

                ordemServico.CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO;
                ordemServico.DataHoraManut = DateTime.Now;
                ordemServico.CodUsuarioManutencao = "INTEGRACAO";

                ordemServico = _ordemServicoService.Atualizar(ordemServico);
                EnviaEmailAprovacaoOrcamento(atendimento, ordemServico.CodOS);
            }
            catch (Exception ex)
            {
                RegistrarExcecao($"Integração Banrisul ATM: Ocorreu um erro {ex.Message}");
            }
        }

        private void ReabrirOS(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
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
                            List<Feriado> feriados = new();

                            if (localAtendimento != null)
                            {
                                Cidade cidade = ordemServico.LocalAtendimento.Cidade;

                                codCidade = localAtendimento.CodCidade;
                                codUF = cidade != null ? cidade.CodUF : 1;
                                feriados = (List<Feriado>)_feriadoService
                                    .ObterPorParametros(new FeriadoParameters
                                    {
                                        CodCidades = codCidade.ToString()
                                    }).Items;
                            }

                            double? horasNaoUteis = CalculaHorasNaoUteis(dataSolucao, DateTime.Now, feriados);
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

                                RegistrarExcecao($"Integração Banrisul ATM: Prazo de 48 horas já encerrado. Status do incidente: REOP. {atendimento.Conteudo}");
                            }
                        }
                        else
                        {
                            EnviaEmailRecusaAbertura(atendimento, "SLA não encontrado: " + equipContrato.CodSLA, emailFilial);

                            RegistrarExcecao($"Integração Banrisul ATM: SLA não encontrada. {atendimento.Conteudo}");
                        }
                    }
                    else
                    {
                        EnviaEmailRecusaAbertura(atendimento, "Chamado não encontrado. Status do incidente: REOP:" + ordemServico.CodContrato, emailFilial);

                        RegistrarExcecao($"Integração Banrisul ATM: Contrato não encontrado: {atendimento.Conteudo}");
                    }

                }
                else
                {
                    EnviaEmailRecusaAbertura(atendimento, "Chamado não encontrado. Status do incidente: REOP.", emailFilial);

                    RegistrarExcecao($"Integração Banrisul ATM: Chamado não encontrado.Status do incidente: REOP. {atendimento.Conteudo}");
                }
            }
            catch (Exception ex)
            {
                RegistrarExcecao($"Integração Banrisul ATM: Erro ao reabrir chamado. {atendimento.Conteudo} Erro {ex.Message}");
            }
        }

        private void AbrirOS(IntegracaoBanrisulAtendimento atendimento)
        {
            OrdemServico ordemServico = new();

            if (string.IsNullOrWhiteSpace(atendimento.DataHoraAbertura.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "A data de abertura é inválida ou não foi informada.", null);

                RegistrarExcecao($"Integração Banrisul ATM: A data de abertura é inválida ou não foi informada. {atendimento.Conteudo}");
            }

            if (atendimento.StatusIncidente.Valor.ToUpper().Equals("CAC"))
            {
                EnviaEmailRecusaAbertura(atendimento, "Chamado de cancelamento ignorado. Status do incidente: CAC.", null);

                RegistrarExcecao($"Integração Banrisul ATM: Chamado de cancelamento ignorado. Status do incidente: CAC. {atendimento.Conteudo}");
            }

            if (string.IsNullOrWhiteSpace(atendimento.NumeroSerie.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar o número de série do equipamento do cliente no e-mail de atendimento.", null);

                RegistrarExcecao($"Integração Banrisul ATM: Não foi possível encontrar o número de série do equipamento do cliente no e-mail de atendimento. {atendimento.Conteudo}");
            }

            EquipamentoContrato equipamento = new();
            Console.WriteLine("Antes do break");

            var equipamentos = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters
            {
                IndAtivo = 1,
                NumSerie = atendimento.NumeroSerie.Valor.Trim().ToUpper(),
                CodClientes = Constants.CLIENTE_BANRISUL.ToString()
            });

            Console.WriteLine("Depois do Break");

            if (equipamentos.Count() == 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "O número de série do equipamento do cliente encontrado no e-mail não está cadastrado no sistema.", null);

                RegistrarExcecao($"Integração Banrisul ATM: O número de série do equipamento do cliente encontrado no e-mail não está cadastrado no sistema. {atendimento.Conteudo}");
            }

            if (equipamentos.Count() > 1)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontrados mais de um equipamento no sistema com o mesmo número de série e local de atendimento.", null);
                
                RegistrarExcecao($"Integração Banrisul ATM: Foram encontrados mais de um equipamento no sistema com o mesmo número de série e local de atendimento. {atendimento.Conteudo}");
            }

            if (equipamentos.Count() == 1)
            {
                equipamento = (EquipamentoContrato)equipamentos.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(atendimento.CodigoLocalEquipamento.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar a agência no e-mail de atendimento.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: Não foi possível encontrar a agência no e-mail de atendimento. {atendimento.Conteudo}");
            }

            if (atendimento.CodigoLocalEquipamento.Valor.Length < 5)
            {
                EnviaEmailRecusaAbertura(atendimento, "O código da agência informado no e-mail deve conter 5 posições.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: O código da agência informado no e-mail deve conter 5 posições. {atendimento.Conteudo}");
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
                EnviaEmailRecusaAbertura(atendimento, "A agência encontrada no e-mail não está cadastrada no sistema.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: A agência encontrada no e-mail não está cadastrada no sistema. {atendimento.Conteudo}");
            }

            if (locais.Count() > 1)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontradas mais de uma agência no sistema com o mesmo código de agência e posto.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: Foram encontradas mais de uma agência no sistema com o mesmo código de agência e posto. {atendimento.Conteudo}");
            }

            if (locais.Count() == 1)
            {
                local = (LocalAtendimento)locais.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(atendimento.NumeroIncidente.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar o número do chamado do cliente no e-mail de atendimento.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: Não foi possível encontrar o número do chamado do cliente no e-mail de atendimento. {atendimento.Conteudo}");
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
                EnviaEmailRecusaAbertura(atendimento, "Já existe um chamado do cliente cadastrado no sistema com este mesmo número de chamado.", equipamento.Filial.Email);
                
                RegistrarExcecao($"Integração Banrisul ATM: Já existe um chamado do cliente cadastrado no sistema com este mesmo número de chamado. {atendimento.Conteudo}");
            }

            if (!string.IsNullOrWhiteSpace(atendimento.DataHoraAgendamento.Valor))
            {
                ordemServico.ObservacaoCliente += Environment.NewLine +
                    String.Format("Agendamento: {0}", DateTime.Parse(atendimento.DataHoraAgendamento.Valor, new CultureInfo("en-US")).ToString("dd/MM/yyyy HH:mm:ss"));
            }

            if (atendimento.DescricaoIncidente.Valor.Contains("RECALL DOS SENSORES FIM DE CÉDULAS DO DISPENSADOR"))
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

            ordemServico = _ordemServicoService.Criar(ordemServico);
            EnviaEmailAbertura(atendimento, ordemServico);
        }

        private void ResponderOS(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
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
                        EnviaEmailResolucao(atendimento, "DataHoraSolucaoValida inválida. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_DATA_INVALIDA);

                        RegistrarExcecao($"Integração Banrisul ATM: DataHoraSolucaoValida inválida. Status do incidente: RE. {atendimento.Conteudo}");
                    }
                }
                else
                {
                    EnviaEmailResolucao(atendimento, "RAT não encontrada. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA);

                    RegistrarExcecao($"Integração Banrisul ATM: RAT não encontrada. Status do incidente: RE. {atendimento.Conteudo}");
                }
            }
            catch (Exception ex)
            {
                RegistrarExcecao($"Integração Banrisul ATM: Erro ao reabrir ordem de serviço. {atendimento.Conteudo} Erro {ex.Message}");
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

        private string GetHtmlEmailTrocaStatus() {
            return string.Empty;
        }

        private void EnviaPdf() {
            
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
            texto.Append("					<td align=\"right\">Número Chamado:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.NumeroIncidente.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Agência:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Posto:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", atendimento.CodigoLocalEquipamento.Valor);
            texto.Append("				</tr>");
            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Número Série Equipamento:</td>");
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

            string mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " recusado pois não atendeu aos seguintes requisitos: " + "<br/><ul><li>" + motivo + "</li></ul>";

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
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução não acatada, Data de resolução inválida.";
                    break;
                case IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA:
                    mensagem = "Chamado " + atendimento.NumeroIncidente.Valor + " - Resolução não acatada, RAT do chamado não encontrada.";
                    break;
            }

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

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

            string mensagem = "Chamado Cliente: " + atendimento.NumeroIncidente.Valor + " com orçamento aprovado de acordo com notificação via integração. " + "<br/> <h5 style='color: steelblue;'>Número OS Perto: 12365478</h5><br/>";

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
            if ("intervencao_CashDesinstalacao".Equals(ClassificacaoAtendimento) ||
                "intervencao_TalonadoraDesinstalacao".Equals(ClassificacaoAtendimento) ||
                "intervencao_TcDesinstalacao".Equals(ClassificacaoAtendimento))
                return 3;
            else if ("intervecao_preventiva".Equals(ClassificacaoAtendimento))
                return 6;
            else if ("intervencao_CashReinstalacao".Equals(ClassificacaoAtendimento) ||
                "intervencao_TalonadoraReinstalacao".Equals(ClassificacaoAtendimento) ||
                "intervencao_TcReinstalacao".Equals(ClassificacaoAtendimento))
                return 7;
            else if ("intervencao_BocalATM".Equals(ClassificacaoAtendimento) || "intervencao_Entitamento".Equals(ClassificacaoAtendimento) ||
                    "intervencao_RecallCDP".Equals(ClassificacaoAtendimento) || "intervencao_k7".Equals(ClassificacaoAtendimento))
                return 1;
            else
                return 2;
        }

        private double? CalculaHorasNaoUteis(DateTime inicio, DateTime fim, List<Feriado> feriados)
        {
            List<DateTime> datas = new List<DateTime>();

            while (inicio.Date <= fim.Date)
            {
                if (fim.DayOfWeek == DayOfWeek.Sunday || fim.DayOfWeek == DayOfWeek.Saturday)
                {
                    datas.Insert(0, fim);
                }
                else
                {
                    var eFeriado = feriados.FirstOrDefault(f => f.Data.ToString().Contains(fim.ToString()));

                    if (eFeriado != null)
                    {
                        datas.Insert(0, fim);
                    }
                }

                fim = fim.AddDays(-1);
            }

            return datas.Count * 24;
        }

        private void RegistrarExcecao(string msg)
        {
            _logger.Error(msg);

            throw new Exception(msg);
        }
    }
}
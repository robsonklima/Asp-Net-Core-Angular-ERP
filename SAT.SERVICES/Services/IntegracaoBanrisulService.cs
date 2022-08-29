using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
        private IEmailService _emailService;
        private IOrdemServicoService _ordemServicoService;
        private IRelatorioAtendimentoService _relatorioAtendimentoService;
        private IEquipamentoContratoService _equipamentoContratoService;
        private ILocalAtendimentoService _localAtendimentoService;

        public IntegracaoBanrisulService(
            IEmailService emailService,
            IOrdemServicoService ordemServicoService,
            IRelatorioAtendimentoService relatorioAtendimentoService,
            IEquipamentoContratoService equipamentoContratoService,
            ILocalAtendimentoService localAtendimentoService
        )
        {
            _emailService = emailService;
            _ordemServicoService = ordemServicoService;
            _relatorioAtendimentoService = relatorioAtendimentoService;
            _equipamentoContratoService = equipamentoContratoService;
            _localAtendimentoService = localAtendimentoService;
        }

        public async void ExecutarAsync()
        {
            var token = await _emailService.ObterTokenAsync();
            var emails = await _emailService.ObterEmailsAsync(token, Constants.EMAIL_TESTE_CONFIG.ClientID);

            foreach (var email in emails.Value)
            {
                var atendimento = Carrega(email.Body.Content);
                
                TentaCadastro(atendimento);

                await _emailService.DeletarEmailAsync(token, Constants.EMAIL_TESTE_CONFIG.ClientID, email.Id);
            }
        }

        private IntegracaoBanrisulAtendimento Carrega(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                throw new Exception("Não é possivel carregar o conteudo pois está vazio.");
            }

            conteudo = StringHelper.GetStringBetweenCharacters(conteudo, '#', '#');

            conteudo = conteudo.Replace("||", "|");

            string[] dados = conteudo.Split('|');

            int quantidadeCampos = dados.Length == 17 ? 17 : 16;

            if (dados.Length != quantidadeCampos)
            {
                throw new Exception("A quantidade de campos encontrados é diferente do permitido. Conteudo encontrado: " + conteudo);
            }

            IntegracaoBanrisulAtendimento atendimento = new();

            atendimento.NumeroIncidente.Valor = dados[atendimento.NumeroIncidente.Indice].ToString();
            atendimento.DataHoraAbertura.Valor = dados[atendimento.DataHoraAbertura.Indice].ToString();
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

            if (atendimento.StatusIncidente.Valor.ToUpper().Equals("RE"))
            {
                string dataHoraSolucao =
                   !string.IsNullOrWhiteSpace(dados[atendimento.DataHoraSolucaoValida.Indice].ToString())
                    ? dados[atendimento.DataHoraSolucaoValida.Indice].ToString()
                    : dados[atendimento.DataHoraSolucaoValida.Indice - 1].ToString();

                atendimento.DataHoraSolucaoValida.Valor = dataHoraSolucao;
            }

            return atendimento;
        }

        private void Reabre(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                OrdemServico ordemServico = (OrdemServico)_ordemServicoService
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        NumOSCliente = atendimento.NumeroIncidente.Valor,
                        CodClientes = Constants.CLIENTE_BANRISUL.ToString()
                    }).Items.FirstOrDefault();

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
                                .SingleOrDefault()
                                .DataHoraSolucao;

                            DateTime hoje = DateTime.Now;
                            bool sabado = (bool)sla.IndSabado;
                            bool domingo = (bool)sla.IndDomingo;
                            bool feriado = (bool)sla.IndFeriado;
                            int codPais = 1;
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

                            // Tempo de 48 horas
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
                                throw new Exception("Prazo de 48 horas já encerrado. Status do incidente: REOP.");
                            }
                        }
                        else
                        {
                            EnviaEmailRecusaAbertura(atendimento, "SLA não encontrado: " + equipContrato.CodSLA, emailFilial);
                            throw new Exception("Contrato não encontrado: " + equipContrato.CodSLA);
                        }
                    }
                    else
                    {
                        EnviaEmailRecusaAbertura(atendimento, "Chamado não encontrado. Status do incidente: REOP:" + ordemServico.CodContrato, emailFilial);
                        throw new Exception("Contrato não encontrado: " + ordemServico.CodContrato);
                    }

                }
                else
                {
                    EnviaEmailRecusaAbertura(atendimento, "Chamado não encontrado. Status do incidente: REOP.", emailFilial);
                    throw new Exception("Chamado não encontrado.Status do incidente: REOP.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao reabrir chamado: {ex.Message}");
            }
        }

        private double? CalculaHorasNaoUteis(DateTime inicio, DateTime fim)
        {
            List<DateTime> dates = new List<DateTime>();
            
            while(inicio.Date <= fim.Date)
            {
                if(fim.DayOfWeek == DayOfWeek.Sunday || fim.DayOfWeek == DayOfWeek.Saturday)
                {
                    dates.Insert(0, fim);
                }
                fim = fim.AddDays(-1);
            }

            return dates.Count * 24;
        }

        private void Responder(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                OrdemServico ordemServico = (OrdemServico)_ordemServicoService
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        NumOSCliente = atendimento.NumeroIncidente.Valor,
                        CodClientes = Constants.CLIENTE_BANRISUL.ToString()
                    }).Items.FirstOrDefault();

                if (ordemServico.RelatoriosAtendimento.Count > 0)
                {
                    RelatorioAtendimento ultimaRAT = ordemServico.RelatoriosAtendimento.OrderByDescending(q => q.CodRAT).FirstOrDefault();
                    DateTime parseDate = new DateTime();
                    if (DateTime.TryParse(atendimento.DataHoraSolucaoValida.Valor, out parseDate))
                    {
                        ultimaRAT.DataHoraSolucaoValida = parseDate;
                        _relatorioAtendimentoService.Atualizar(ultimaRAT);

                        EnviaEmailResolucao(atendimento, "Resolução acatada com sucesos", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RECEBIDA);
                        return;
                    }
                    else
                    {
                        EnviaEmailResolucao(atendimento, "DataHoraSolucaoValida inválida. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_DATA_INVALIDA);
                        throw new Exception("DataHoraSolucaoValida inválida. Status do incidente: RE.");
                    }
                }
                else
                {
                    EnviaEmailResolucao(atendimento, "RAT não encontrada. Status do incidente: RE.", ordemServico.Filial.Email, IntegracaoBanrisulResolucaoEnum.RESOLUCAO_RAT_NAO_ENCONTRADA);
                    throw new Exception("RAT não encontrada. Status do incidente: RE.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao reabrir ordem de serviço {ex.Message}");
            }
        }

        private void TentaCadastro(IntegracaoBanrisulAtendimento atendimento)
        {
            try
            {
                if (atendimento.StatusIncidente.Valor.ToUpper().Equals("REOP"))
                {
                    Reabre(atendimento);
                }
                else if (atendimento.StatusIncidente.Valor.ToUpper().Equals("RE"))
                {
                    Responder(atendimento);
                }
                else
                {
                    Abre(atendimento);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Abre(IntegracaoBanrisulAtendimento atendimento)
        {
            OrdemServico ordemServico = new();

            if (string.IsNullOrWhiteSpace(atendimento.DataHoraAbertura.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "A data de abertura é inválida ou não foi informada.", ordemServico.Filial.Email);
                throw new Exception("A data de abertura é inválida ou não foi informada.");
            }

            if (atendimento.StatusIncidente.Valor.ToUpper().Equals("CAC"))
            {
                EnviaEmailRecusaAbertura(atendimento, "Chamado de cancelamento ignorado. Status do incidente: CAC.", ordemServico.Filial.Email);
                throw new Exception("Chamado de cancelamento ignorado. Status do incidente: CAC.");
            }

            if (string.IsNullOrWhiteSpace(atendimento.NumeroSerie.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar o número de série do equipamento do cliente no e-mail de atendimento.", ordemServico.Filial.Email);
                throw new Exception("Não foi possível encontrar o número de série do equipamento do cliente no e-mail de atendimento.");
            }

            ordemServico.EquipamentoContrato.NumSerie = atendimento.NumeroSerie.Valor.Trim().ToUpper();
            ordemServico.EquipamentoContrato.IndAtivo = (int)StatusServicoEnum.ABERTO;

            EquipamentoContrato equipamento = new();

            var equipamentos = _equipamentoContratoService.ObterPorParametros(new EquipamentoContratoParameters
            {
                IndAtivo = 1,
                NumSerie = atendimento.NumeroSerie.Valor.Trim().ToUpper(),
                CodClientes = Constants.CLIENTE_BANRISUL.ToString()
            }).Items;

            if (equipamentos.Count() == 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "O número de série do equipamento do cliente encontrado no e-mail não está cadastrado no sistema.", ordemServico.Filial.Email);
                throw new Exception("O número de série do equipamento do cliente encontrado no e-mail não está cadastrado no sistema.");
            }

            if (equipamentos.Count() > 1)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontrados mais de um equipamento no sistema com o mesmo número de série e local de atendimento.", ordemServico.Filial.Email);
                throw new Exception("Foram encontrados mais de um equipamento no sistema com o mesmo número de série e local de atendimento.");
            }

            if (equipamentos.Count() == 1)
            {
                equipamento = (EquipamentoContrato)equipamentos.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(atendimento.CodigoLocalEquipamento.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar a agência no e-mail de atendimento.", ordemServico.Filial.Email);
                throw new Exception("Não foi possível encontrar a agência no e-mail de atendimento.");
            }

            if (atendimento.CodigoLocalEquipamento.Valor.Length < 5)
            {
                EnviaEmailRecusaAbertura(atendimento, "O código da agência informado no e-mail está ilegível.", ordemServico.Filial.Email);
                throw new Exception("O código da agência informado no e-mail está ilegível.");
            }

            ordemServico.LocalAtendimento.NumAgencia = atendimento.CodigoLocalEquipamento.Valor;
            ordemServico.LocalAtendimento.Cliente.CodCliente = Constants.CLIENTE_BANRISUL;

            LocalAtendimento local = new();

            var locais = _localAtendimentoService.ObterPorParametros(new LocalAtendimentoParameters
            {
                IndAtivo = 1,
                CodClientes = Constants.CLIENTE_BANRISUL.ToString(),
                NumAgencia = atendimento.CodigoLocalEquipamento.Valor,
                CodPosto = ordemServico.EquipamentoContrato.CodPosto
            }).Items;

            if (locais.Count() == 0)
            {
                locais = _localAtendimentoService.ObterPorParametros(new LocalAtendimentoParameters
                {
                    IndAtivo = 1,
                    CodClientes = Constants.CLIENTE_BANRISUL.ToString(),
                    CodPosto = ordemServico.EquipamentoContrato.CodPosto
                }).Items.ToList();
            }

            if (locais.Count() == 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "A agência encontrada no e-mail não está cadastrada no sistema.", ordemServico.Filial.Email);
                throw new Exception("A agência encontrada no e-mail não está cadastrada no sistema.");
            }

            if (locais.Count() > 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "Foram encontradas mais de uma agência no sistema com o mesmo código de agência e posto.", ordemServico.Filial.Email);
                throw new Exception("Foram encontradas mais de uma agência no sistema com o mesmo código de agência e posto.");
            }

            if (locais.Count() == 1)
            {
                local = (LocalAtendimento)locais.FirstOrDefault();
            }

            ordemServico.EquipamentoContrato.LocalAtendimento.CodPosto = local.CodPosto;
            ordemServico.Autorizada.CodAutorizada = equipamento.CodAutorizada;
            ordemServico.Equipamento.CodEquip = (int)equipamento.CodEquip;
            ordemServico.EquipamentoContrato.CodEquipContrato = equipamento.CodEquipContrato;
            ordemServico.Filial.CodFilial = equipamento.CodFilial;
            ordemServico.CodGrupoEquip = equipamento.CodGrupoEquip;
            ordemServico.Regiao.CodRegiao = equipamento.CodRegiao;
            ordemServico.CodTipoEquip = equipamento.CodTipoEquip;

            if (string.IsNullOrWhiteSpace(atendimento.NumeroIncidente.Valor))
            {
                EnviaEmailRecusaAbertura(atendimento, "Não foi possível encontrar o número do chamado do cliente no e-mail de atendimento.", ordemServico.Filial.Email);
                throw new Exception("Não foi possível encontrar o número do chamado do cliente no e-mail de atendimento.");
            }

            ordemServico.NumOSCliente = atendimento.NumeroIncidente.Valor;

            var chamados = _ordemServicoService.ObterPorParametros(new OrdemServicoParameters
            {
                NumOSCliente = atendimento.NumeroIncidente.Valor,
                CodClientes = Constants.CLIENTE_BANRISUL.ToString(),
                NotIn_CodStatusServicos = StatusServicoEnum.CANCELADO.ToString(),
                SortActive = "CodOS",
                SortDirection = "DESC"
            }).Items;

            if (chamados.Count() > 0)
            {
                EnviaEmailRecusaAbertura(atendimento, "Já existe um chamado do cliente cadastrado no sistema com este mesmo número de chamado.", ordemServico.Filial.Email);
                throw new Exception("Já existe um chamado do cliente cadastrado no sistema com este mesmo número de chamado.");
            }

            ordemServico.DefeitoRelatado = atendimento.DescricaoIncidente.Valor;
            ordemServico.ObservacaoCliente = String.Format("Horário de Atendimento: {0}", atendimento.HorarioAtendimento.Valor);

            if (!string.IsNullOrWhiteSpace(atendimento.DataHoraAgendamento.Valor))
            {
                ordemServico.ObservacaoCliente += Environment.NewLine +
                    String.Format("Agendamento: {0}", DateTime.Parse(atendimento.DataHoraAgendamento.Valor, new CultureInfo("en-US")).ToString("dd/MM/yyyy HH:mm:ss"));
            }

            ordemServico.NomeSolicitante = atendimento.NomeContato.Valor;
            ordemServico.CodPosto = ordemServico.EquipamentoContrato.CodPosto;
            ordemServico.StatusServico.CodStatusServico = (int)StatusServicoEnum.ABERTO;


            ordemServico.TipoIntervencao.CodTipoIntervencao = GeraTipoIntervencao(atendimento.ClassificacaoAtendimento.Valor.ToUpper());
            if (atendimento.DescricaoIncidente.Valor.Contains("RECALL DOS SENSORES FIM DE CÉDULAS DO DISPENSADOR"))
            {
                ordemServico.TipoIntervencao.CodTipoIntervencao = (int)StatusServicoEnum.ABERTO;
                ordemServico.NumOSQuarteirizada = "REC FIM DE CED";
            }
            ordemServico.IndServico = 1;
            ordemServico.IndIntegracao = 1;
            ordemServico.DataHoraAberturaOS = DateTime.Now;
            ordemServico.DataHoraCad = DateTime.Now;
            ordemServico.CodUsuarioCad = "INTEGRACAO";

            ordemServico.DataHoraSolicitacao = DateTime.Parse(atendimento.DataHoraAbertura.Valor);

            _ordemServicoService.Criar(ordemServico);
            EnviaEmailAbertura(atendimento, ordemServico);
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

            _emailService.Enviar(new Email {
                EmailDestinatario = "equipe.sat@perto.com.br;giane.santos@perto.com.br;",
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

            _emailService.Enviar(new Email {
                EmailDestinatario = "equipe.sat@perto.com.br;giane.santos@perto.com.br;",
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

            _emailService.Enviar(new Email {
                EmailDestinatario = "equipe.sat@perto.com.br;giane.santos@perto.com.br" + ";" + destinatario,
                Assunto = assunto,
                Corpo = texto
            });
        }

        private void EnviaEmailAprovacaoOrcamento(IntegracaoBanrisulAtendimento atendimento, string CodOS)
        {
            string assunto = "Integração Banrisul - Aprovação Orçamento - OS: " + CodOS + ".";

            string mensagem = "Chamado Cliente: " + atendimento.NumeroIncidente.Valor + " com orçamento aprovado de acordo com notificação via integração. " + "<br/> <h5 style='color: steelblue;'>Número OS Perto: 12365478</h5><br/>";

            string texto = GetHtmlEmailAbertura(atendimento, mensagem);

            _emailService.Enviar(new Email {
                EmailDestinatario = "equipe.sat@perto.com.br;giane.santos@perto.com.br;rosimar.silva@perto.com.br;dss.orcamentos@perto.com.br",
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
    }
}
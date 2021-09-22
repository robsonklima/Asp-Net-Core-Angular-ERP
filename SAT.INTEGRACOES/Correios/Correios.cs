using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using SAT.INTEGRACOES.Correios.Models;
using SAT.INTEGRACOES.Enums;
using SAT.INTEGRACOES.Extensions;
using SAT.INTEGRACOES.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SAT.INTEGRACOES.Correios
{
    public class Correios : BaseIntegracao, IIntegracao
    {
        #region Base Integração

        /// <summary>
        /// Verificar novas integrações da integração #1
        /// </summary>
        public void VerificaNovaIntegracao()
        {
            try
            {
                if (this.sistemaHabilitado)
                {
                    // Busca os contratos
                    List<Contrato> __listaContratos = this.BuscaRespostaConexaoServidor<List<Contrato>>(URLRequestEnum.CONTRATOS).Data;

                    // Busca os chamados para os contratos
                    foreach (Contrato item in __listaContratos)
                    {
                        string contrato = item.VALUE2;
                        List<Chamado> __listaChamados = new List<Chamado>();

                        /* Verifica chamados abertos */
                        IRestResponse<List<Chamado>> responseChamadosAbertos = this.BuscaRespostaConexaoServidor<List<Chamado>>(URLRequestEnum.CHAMADOS_ABERTOS, contrato);
                        __listaChamados.AddRange(JsonConvert.DeserializeObject<List<Chamado>>(responseChamadosAbertos.Content));

                        // Cadastra os chamados
                        foreach (Chamado chamado in __listaChamados)
                        {
                            if (chamado.OS == null)
                            {
                                LoggerService.LogWarn($"({base.NomeIntegracao}) - Nenhum chamado encontrado para o contrato numero {contrato}.");
                            }
                            else
                            {
                                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.REGISTRADO, contexto: chamado, mensagemAviso: "ABERTO");
                            }
                        }

                        __listaChamados.Clear();

                        /* Verifica chamados atribuidos */
                        IRestResponse<List<Chamado>> responseChamadosAtribuidos = this.BuscaRespostaConexaoServidor<List<Chamado>>(URLRequestEnum.CHAMADOS_ATRIBUIDOS, contrato);
                        __listaChamados.AddRange(JsonConvert.DeserializeObject<List<Chamado>>(responseChamadosAtribuidos.Content));

                        // Cadastra os chamados
                        foreach (Chamado chamado in __listaChamados)
                        {
                            if (chamado.OS == null)
                            {
                                LoggerService.LogWarn($"({base.NomeIntegracao}) - Nenhum chamado encontrado para o contrato numero {contrato}.");
                            }
                            else
                            {
                                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.REGISTRADO, contexto: chamado, mensagemAviso: "ATRIBUIDO");
                            }
                        }

                        /* Verifica chamados encerrados */
                        List<OrdemServico> __chamadosEncerrados = base.DbContext.OrdemServico.Where(c => c.CodCliente == this.codClienteCorreios)
                                                                 .Include(r => r.RelatoriosAtendimento.Where(rat => rat.CodStatusServico == 3))
                                                                 .Include(t => t.Tecnico)
                                                                 .ToList();

                        foreach (OrdemServico chamado in __chamadosEncerrados)
                        {
                            base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.REGISTRADO, contexto: chamado, mensagemAviso: "ENCERRADOS");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"({base.NomeIntegracao}) - Erro ao verificar novas integrações. - Exception: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Tranformar os dados recebidos na estrutura Perto #2
        /// </summary>
        public void TransformaDadosIntegracao()
        {
            try
            {
                List<Integracao> __requisicoes = base.BuscaDadosIntegracaoEmFila();

                foreach (Integracao integracao in __requisicoes)
                {
                    // Seta na base a integracao que está trabalhando
                    base.Integracao = integracao;

                    // Busca a requisição 
                    object __requisicao = base.BuscaRequisicao<object>();

                    // Requisição de chamado
                    if ((Chamado)__requisicao != null)
                    {
                        Chamado __chamado = (Chamado)__requisicao;

                        OrdemServico __integracaoOS = new OrdemServico();

                        __integracaoOS.LocalAtendimento.Cliente.CodCliente = this.codClienteCorreios;
                        __integracaoOS.EquipamentoContrato.NumSerie = __chamado.N_SERIE.Trim().ToUpper();
                        __integracaoOS.EquipamentoContrato.IndAtivo = 1;

                        __integracaoOS.DefeitoRelatado = __chamado.DESCRICAO_TOTAL;

                        __integracaoOS.StatusServico.CodStatusServico = 1;
                        __integracaoOS.TipoIntervencao.CodTipoIntervencao = 2;
                        __integracaoOS.IndServico = 1;
                        __integracaoOS.IndIntegracao = 1;
                        __integracaoOS.DataHoraAberturaOS = DateTime.Now;
                        __integracaoOS.DataHoraCad = DateTime.Now;
                        __integracaoOS.CodUsuarioCad = "INTEGRACAO";
                        __integracaoOS.NumOSCliente = __chamado.OS;
                        __integracaoOS.DataHoraSolicitacao = DateTime.Parse(__chamado.ABERTURA_OS);

                        Atendimento atendimento = this.BuscaRespostaConexaoServidor<Atendimento>(URLRequestEnum.ATENDIMENTOS, ordemServico: __chamado.OS).Data;
                        __integracaoOS.ObservacaoCliente = "Horario de atendimento: " + atendimento.HR_INI + " / " + atendimento.HR_FIM + ". Intervalo: " + atendimento.ALMOCO_INI + " / " + atendimento.ALMOCO_FIM + ".";
                        __integracaoOS.NomeSolicitante = atendimento.NOME_CLIENTE;

                        EquipamentoContrato equipamentoCorreios = base.DbContext.EquipamentoContrato.FirstOrDefault(eq => (eq.NumSerieCliente == __chamado.N_SERIE.Trim() || eq.NumSerie == __chamado.N_SERIE.Trim()) && eq.CodCliente == this.codClienteCorreios && eq.IndAtivo == 1);

                        if (equipamentoCorreios != null)
                        {
                            __integracaoOS.CodAutorizada = equipamentoCorreios.CodAutorizada;
                            __integracaoOS.Equipamento.CodEquip = equipamentoCorreios.CodEquip;
                            __integracaoOS.EquipamentoContrato.CodEquipContrato = equipamentoCorreios.CodEquipContrato;
                            __integracaoOS.Filial.CodFilial = equipamentoCorreios.CodFilial;
                            __integracaoOS.CodGrupoEquip = equipamentoCorreios.CodGrupoEquip;
                            __integracaoOS.RegiaoAutorizada.CodRegiao = equipamentoCorreios.CodRegiao;
                            __integracaoOS.CodTipoEquip = equipamentoCorreios.CodTipoEquip;
                            __integracaoOS.EquipamentoContrato.LocalAtendimento.CodPosto = equipamentoCorreios.CodPosto;
                            __integracaoOS.LocalAtendimento.CodPosto = equipamentoCorreios.CodPosto;
                        }

                        base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.EM_FILA_DE_EXECUCAO, contexto: __integracaoOS, mensagemAviso: __chamado.ESTADO);
                    }

                    // Fechamento de chamado
                    if ((OrdemServico)__requisicao != null)
                    {
                        OrdemServico __fechamento = (OrdemServico)__requisicao;

                        (from rat in __fechamento.RelatoriosAtendimento
                         select new OSEncerrada()
                         {
                             chc_co_tecnico = __fechamento.Tecnico.Cpf,
                             contrato = this.codContratoServer,
                             cpf_tecnico = __fechamento.Tecnico.Cpf,
                             dh_ini_servico = rat.DataHoraInicio.ToString(),
                             erp_nu_os = __fechamento.NumOSCliente,
                             nome_tecnico = __fechamento.Tecnico.Nome,
                             nu_os_contratada = __fechamento.CodOS.ToString(),
                             obs_contratada = rat.ObsRAT,
                             solucao = rat.RelatoSolucao,
                             tel_tecnico = __fechamento.Tecnico.FonePerto,
                             tp_atendimento = "S",
                             tp_fechamento = "C"
                         }).ToList().ForEach(chamado =>
                         {
                             base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.EM_FILA_DE_EXECUCAO, contexto: chamado, mensagemAviso: "ENCERRADO");
                         });
                    }
                }
            }
            catch (Exception ex)
            {
                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: "Integração não pode transformar os dados");
                LoggerService.LogError($"({base.NomeIntegracao}) - Erro ao transformar novas integrações. - Exception: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Executa a requisição da integração #3
        /// </summary>
        public void ExecutaIntegracao()
        {
            try
            {
                List<Integracao> __requisicoes = base.BuscaDadosIntegracaoPreparadosParaExecucao();

                foreach (Integracao integracao in __requisicoes)
                {
                    base.Integracao = integracao;

                    base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.EM_EXECUCAO);

                    // Busca a requisição 
                    object __requisicao = base.BuscaRequisicao<object>();

                    // Requisição de novo chamado
                    if ((OrdemServico)__requisicao != null)
                    {
                        OrdemServico execucaoOS = (OrdemServico)__requisicao;

                        EquipamentoContrato __equipamentoCorreios = execucaoOS.EquipamentoContrato;

                        if (__equipamentoCorreios != null)
                        {
                            if (!base.DbContext.OrdemServico.Any(os => os.NumOSCliente == execucaoOS.NumOSCliente && os.CodCliente == this.codClienteCorreios))
                            {
                                OrdemServico __ordemServico = new OrdemServico();

                                __ordemServico.LocalAtendimento.Cliente.CodCliente = this.codClienteCorreios;
                                __ordemServico.EquipamentoContrato.NumSerie = execucaoOS.EquipamentoContrato.NumSerie;
                                __ordemServico.EquipamentoContrato.IndAtivo = 1;

                                __ordemServico.CodAutorizada = __equipamentoCorreios.CodAutorizada;
                                __ordemServico.Equipamento.CodEquip = __equipamentoCorreios.CodEquip;
                                __ordemServico.EquipamentoContrato.CodEquipContrato = __equipamentoCorreios.CodEquipContrato;
                                __ordemServico.Filial.CodFilial = __equipamentoCorreios.CodFilial;
                                __ordemServico.CodGrupoEquip = __equipamentoCorreios.CodGrupoEquip;
                                __ordemServico.RegiaoAutorizada.CodRegiao = __equipamentoCorreios.CodRegiao;
                                __ordemServico.CodTipoEquip = __equipamentoCorreios.CodTipoEquip;
                                __ordemServico.EquipamentoContrato.LocalAtendimento.CodPosto = __equipamentoCorreios.CodPosto;
                                __ordemServico.DefeitoRelatado = execucaoOS.DefeitoRelatado;

                                __ordemServico.LocalAtendimento.CodPosto = __equipamentoCorreios.CodPosto;
                                __ordemServico.StatusServico.CodStatusServico = 1;
                                __ordemServico.TipoIntervencao.CodTipoIntervencao = 2;
                                __ordemServico.IndServico = 1;
                                __ordemServico.IndIntegracao = 1;
                                __ordemServico.DataHoraAberturaOS = DateTime.Now;
                                __ordemServico.DataHoraCad = DateTime.Now;
                                __ordemServico.CodUsuarioCad = "INTEGRACAO";
                                __ordemServico.NumOSCliente = execucaoOS.NumOSCliente;
                                __ordemServico.DataHoraSolicitacao = execucaoOS.DataHoraSolicitacao;

                                __ordemServico.ObservacaoCliente = execucaoOS.ObservacaoCliente;
                                __ordemServico.NomeSolicitante = execucaoOS.NomeSolicitante;

                                __ordemServico.CodOS = base.SequenciaRepository.ObterContador("OrdemServico");

                                base.DbContext.OrdemServico.Add(__ordemServico);
                                base.DbContext.SaveChanges();

                                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.AGUARDANDO_ENVIO_RETORNO);
                                LoggerService.LogInfo($"({base.NomeIntegracao}) - Chamado Correios Nº: Integração: {__ordemServico.NumOSCliente} integrado com sucesso. Chamado Perto Nº: {__ordemServico.CodOS }.");
                            }
                            else
                            {
                                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: $"Número de chamado do cliente {execucaoOS.NumOSCliente} duplicado.");
                                LoggerService.LogError($"({base.NomeIntegracao}) - Número de chamado do cliente {execucaoOS.NumOSCliente} duplicado.");
                            }
                        }
                        else
                        {
                            base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: $"Número de série {execucaoOS.NumOSCliente} não encontrado na base de dados do SAT.");
                            LoggerService.LogError($"({base.NomeIntegracao}) - Número de série {execucaoOS.NumOSCliente} não encontrado na base de dados do SAT.");
                        }
                    }

                    // Transferencia de chamado
                    if ((Chamado)__requisicao != null)
                    {
                        Chamado __chamado = (Chamado)__requisicao;

                        // TRANSFERIDOS
                        if (__chamado.ESTADO.Equals("ATRIBUIDO"))
                        {
                            List<OrdemServico> __listaChamadosCorreios = this.BuscaListaChamadosTecnicos();

                            List<Tecnico> __listaTecnicosCadastradosBase = __listaChamadosCorreios.Select(t => t.Tecnico).ToList();

                            List<TecnicoLista> __listaTecnicosCadastradosCorreios = this.BuscaRespostaConexaoServidor<List<TecnicoLista>>(URLRequestEnum.TECNICOS).Data;

                            foreach (Tecnico tecnico in __listaTecnicosCadastradosBase)
                            {
                                string loginTecnico = Regex.Replace(tecnico.Cpf.ToString(), "[^.0-9]", "");
                                if (__listaTecnicosCadastradosCorreios.Any(tec => tec.TEC_CO_TECNICO.Equals(loginTecnico)))
                                {
                                    string[] listaChamadosTecnico = __listaChamadosCorreios.Where(s => s.Tecnico == tecnico).Select(q => q.NumOSCliente).ToArray();

                                    if (!listaChamadosTecnico.Contains(__chamado.OS))
                                    {
                                        TecnicoLista __infoTecnico = __listaTecnicosCadastradosCorreios.FirstOrDefault(f => f.TEC_CO_TECNICO.Equals(loginTecnico));

                                        if (__infoTecnico == null)
                                        {
                                            base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: "Integração não pode executar os dados, informações do técnico invalidas");
                                            LoggerService.LogError($"({base.NomeIntegracao}) - Integração não pode executar os dados, informações do técnico invalidas. - {loginTecnico}");
                                            continue;
                                        }

                                        TecnicoJson __tecnicoJson = new TecnicoJson()
                                        {
                                            erp_nu_os = __chamado.OS,
                                            cidade = __infoTecnico.CID_CO_CID_POLO,
                                            chc_co_tecnico = loginTecnico,
                                            nome_tecnico = tecnico.Nome,
                                            cpf_tecnico = tecnico.Cpf,
                                            tel_tecnico = tecnico.FonePerto,
                                            responsavel = "001"
                                        };

                                        Response response = this.BuscaRespostaConexaoServidor<Response>(URLRequestEnum.ATRIBUI_CHAMADO_TECNICO, tecnico: __tecnicoJson).Data;
                                        base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.COMPLETO, mensagemAviso: response.mensagem);
                                        LoggerService.LogInfo($"({base.NomeIntegracao}) - chamado atribuido ao técnico da integração. - Mensagem: {response.mensagem}");
                                    }
                                }// Técnico não encontrado
                                else
                                {
                                    TecnicoJson __tecnicoJson = new TecnicoJson()
                                    {
                                        erp_nu_os = __chamado.OS,
                                        chc_co_tecnico = loginTecnico,
                                        nome_tecnico = tecnico.Nome,
                                        cpf_tecnico = tecnico.Cpf,
                                        tel_tecnico = tecnico.FonePerto,
                                        rg_tecnico = tecnico.Rg,
                                        email_tecnico = tecnico.Email,
                                        responsavel = "001"
                                    };

                                    base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.AGUARDANDO_ENVIO_AVISO_TECNICO_NAO_CADASTRADO, contexto: __tecnicoJson, mensagemAviso: $"Técnico não cadastrado {tecnico.Nome} - {__chamado.OS}");
                                    LoggerService.LogError($"({base.NomeIntegracao}) -Técnico não cadastrado na integração. - Técnico: {tecnico.Nome} - {__chamado.OS}");

                                    //  AtendimentoNotificacao.EnviaEmailCadastroTecnico(tech);
                                }
                            }
                        }
                    }

                    // Encerramento de chamado
                    if ((OSEncerrada)__requisicao != null)
                    {
                        OSEncerrada __chamadoEncerrado = (OSEncerrada)__requisicao;

                        List<TecnicoLista> __listaTecnicosCadastradosCorreios = this.BuscaRespostaConexaoServidor<List<TecnicoLista>>(URLRequestEnum.TECNICOS).Data;

                        string loginTecnico = Regex.Replace(__chamadoEncerrado.cpf_tecnico.ToString(), "[^.0-9]", "");

                        TecnicoLista __infoTecnico = __listaTecnicosCadastradosCorreios.FirstOrDefault(f => f.TEC_CO_TECNICO.Equals(loginTecnico));

                        if (__infoTecnico == null)
                        {
                            base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: "Integração não pode executar os dados, informações do técnico invalidas");
                            LoggerService.LogError($"({base.NomeIntegracao}) - Integração não pode executar os dados, informações do técnico invalidas. - {loginTecnico}");
                            continue;
                        }

                        TecnicoJson tecnicoJson = new TecnicoJson()
                        {
                            cidade = __infoTecnico.CID_CO_CID_POLO
                        };

                        Response response = this.BuscaRespostaConexaoServidor<Response>(URLRequestEnum.CHAMADOS_ENCERRADOS, tecnico: tecnicoJson, chamadoEncerrado: __chamadoEncerrado).Data;
                        base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.COMPLETO, mensagemAviso: response.mensagem);
                        LoggerService.LogInfo($"({base.NomeIntegracao}) - chamado encerrado pela integração. - Mensagem: {response.mensagem}");
                    }
                }
            }
            catch (Exception ex)
            {
                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: "Integração não pode executar os dados");
                LoggerService.LogError($"({base.NomeIntegracao}) - Erro ao executar os dados da integração. - Exception: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Retorna as respostas para a integração #4
        /// </summary>
        public void RetornoIntegracao()
        {
            try
            {
                // Correios não tem retorno ?
                List<Integracao> __requisicoes = base.BuscaDadosIntegracaoEnviarRetorno();

                foreach (Integracao integracao in __requisicoes)
                {
                    StatusIntegracaoEnum __statusIntegracaoEnum = (StatusIntegracaoEnum)Enum.Parse(typeof(StatusIntegracaoEnum), integracao.StatusIntegracao);

                    switch (__statusIntegracaoEnum)
                    {
                        case StatusIntegracaoEnum.AGUARDANDO_ENVIO_RETORNO:
                            break;
                        case StatusIntegracaoEnum.AGUARDANDO_ENVIO_AVISO_TECNICO_NAO_CADASTRADO:
                            {
                                base.Integracao = integracao;

                                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ENVIANDO_AVISO_TECNICO_NAO_CADASTRADO);

                                // Busca a requisição 
                                object __requisicao = base.BuscaRequisicao<object>();

                                TecnicoJson __tecnico = (TecnicoJson)__requisicao;

                                string assunto = "Integração Correios - Técnico não cadastrado";

                                string mensagem = $"Necessário cadastro do técnico {__tecnico.nome_tecnico}. <br>Chamado OSCliente: {__tecnico.erp_nu_os} não transferido no sistema GST Correios devido a falta de cadastro.<br> Email de notificação encaminhado para: {this.mailNotificacaoTecnico}.";

                                string texto = GetHtmlEmailTecnicoNaoCadastrado(mensagem, __tecnico, __tecnico.erp_nu_os);

                                base.EmailService.EnviaEmail(
                                  strMailFrom: "equipe.sat@perto.com.br",
                                  strMailTo: this.mailNotificacaoTecnico,
                                  strSubject: assunto,
                                  strBody: texto.Replace(">", ">" + Environment.NewLine).Replace("}.", "}" + Environment.NewLine + "."),
                                  strMailFormat: "html");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                base.AtualizaFilaIntegracoes(StatusIntegracaoEnum.ERRO, mensagemAviso: "Integração não pode enviar o retorno dos dados");
                LoggerService.LogError($"({base.NomeIntegracao}) - Erro ao executar o retorno de dados. - Exception: {ex.InnerException.Message}");
            }
        }

        #endregion

        #region Integração Correios

        private string uri = ConfigurationManager.AppSettings["Uri"];
        private string key = ConfigurationManager.AppSettings["Key"];
        private string contentType = ConfigurationManager.AppSettings["ContentType"];
        private TimeSpan sistemaExecucaoAgendamentoHorarioInicio = TimeSpan.Parse(ConfigurationManager.AppSettings["sistema.execucao.agendamento.horario_inicio"]);
        private TimeSpan sistemaExecucaoAgendamentoHorarioFim = TimeSpan.Parse(ConfigurationManager.AppSettings["sistema.execucao.agendamento.horario_fim"]);
        private bool sistemaExecucaoAgendamentoHabilitado = bool.Parse(ConfigurationManager.AppSettings["sistema.execucao.agendamento.habilitado"]);
        private string urlListaChamados = "chamados/lista_chamados_direcionados/?contrato={0}&dir_co_dr=&status={1}&data_inicial={2}&data_final={3}";
        private string urlInsereTecnicoServer = "tecnicos/insere_tecnico";
        private string urllistaTechServer = "tecnicos/lista?contrato=000265_2018_001";
        private string urlAtribui_tecnico_chamado = "chamados_novo/atribui_tecnico_chamado";
        private string urlContratos = "contratos/000265_2018_001";
        private string urlAtendimento = "chamados/lista_chamado";
        private string urlFecha_chamado_tecnico = "chamados_novo/fecha_chamado_tecnico";
        private string urlListaTecnicoCadastradoCorreios = "tecnicos/lista?contrato=000265_2018_001";
        private string mailNotificacaoTecnico = ConfigurationManager.AppSettings["destinatario.email.retorno.tecnico.nao.cadastrado"];
        private string codContratoServer = ConfigurationManager.AppSettings["codigo.contrato.servico.correios"];
        private int codClienteCorreios = 209;

        private bool sistemaHabilitado
        {
            get
            {
                {
                    if (this.sistemaExecucaoAgendamentoHabilitado)
                    {
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                        {
                            LoggerService.LogWarn($"Fora do horário agendado. Chamados não são abertos no final de semana. Integração: {base.NomeIntegracao}");
                            return false;
                        }
                        else
                        {
                            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;

                            if (horarioAtual < this.sistemaExecucaoAgendamentoHorarioInicio ||
                                horarioAtual > this.sistemaExecucaoAgendamentoHorarioFim)
                            {
                                LoggerService.LogWarn($"Fora do horário agendado. Integração: {base.NomeIntegracao}");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                        return false;
                }
            }
        }

        private IRestResponse<T> BuscaRespostaConexaoServidor<T>(URLRequestEnum uRLRequestEnum, string contrato = null, string ordemServico = null, TecnicoJson tecnico = null, OSEncerrada chamadoEncerrado = null) where T : class
        {
            RestClient client = new RestClient(this.uri);
            RestRequest request = null;

            switch (uRLRequestEnum)
            {
                case URLRequestEnum.CONTRATOS:
                    request = new RestRequest(this.urlContratos, Method.GET);
                    break;
                case URLRequestEnum.CHAMADOS_ABERTOS:
                    {
                        string uri = String.Format(this.urlListaChamados, contrato, ChamadosCorreiosEnum.CHAMADOS_ABERTOS.StringValue(),
                            string.Empty /*Data Inicial*/, string.Empty /*Data Final*/);

                        request = new RestRequest(this.uri, Method.GET);
                    }
                    break;
                case URLRequestEnum.CHAMADOS_ATRIBUIDOS:
                    {
                        string uri = String.Format(this.urlAtribui_tecnico_chamado, contrato, ChamadosCorreiosEnum.CHAMADOS_ATRIBUIDOS.StringValue(),
                            string.Empty /*Data Inicial*/, string.Empty /*Data Final*/);

                        request = new RestRequest(this.uri, Method.GET);
                    }
                    break;
                case URLRequestEnum.ATENDIMENTOS:
                    {
                        string uri = String.Format(this.urlAtendimento, ordemServico);
                        request = new RestRequest(this.uri, Method.GET);
                    }
                    break;
                case URLRequestEnum.TECNICOS:
                    request = new RestRequest(this.urlListaTecnicoCadastradoCorreios, Method.GET);
                    break;
                case URLRequestEnum.ATRIBUI_CHAMADO_TECNICO:
                    {
                        request = new RestRequest(this.uri, Method.PUT);
                        request.AddJsonBody(new
                        {
                            erp_nu_os = tecnico.erp_nu_os,
                            cidade = tecnico.cidade,
                            chc_co_tecnico = tecnico.chc_co_tecnico,
                            nome_tecnico = tecnico.nome_tecnico,
                            cpf_tecnico = tecnico.cpf_tecnico,
                            tel_tecnico = tecnico.tel_tecnico,
                            responsavel = tecnico.responsavel
                        });
                    }
                    break;
                case URLRequestEnum.CHAMADOS_ENCERRADOS:
                    {
                        request = new RestRequest(this.uri, Method.PUT);
                        request.AddJsonBody(new
                        {
                            erp_nu_os = chamadoEncerrado.erp_nu_os,
                            contrato = chamadoEncerrado.contrato,
                            cidade = tecnico.cidade,
                            chc_co_tecnico = chamadoEncerrado.chc_co_tecnico.Replace("(", "").Replace(")", "").Replace("-", ""),
                            nome_tecnico = chamadoEncerrado.nome_tecnico,
                            cpf_tecnico = chamadoEncerrado.cpf_tecnico.Replace("(", "").Replace(")", "").Replace("-", ""),
                            tel_tecnico = chamadoEncerrado.tel_tecnico.Replace("(", "").Replace(")", "").Replace("-", ""),
                            tp_atendimento = chamadoEncerrado.tp_atendimento,
                            solucao = chamadoEncerrado.solucao,
                            tp_fechamento = chamadoEncerrado.tp_fechamento,
                            nu_os_contratada = chamadoEncerrado.nu_os_contratada,
                            path_email_abertura = chamadoEncerrado.path_email_abertura,
                            dh_ini_servico = chamadoEncerrado.dh_ini_servico,
                            obs_contratada = chamadoEncerrado.obs_contratada,
                            item = chamadoEncerrado.item
                        });
                    }
                    break;
            }

            request.AddHeader("Content-Type", this.contentType);
            request.AddHeader("Key", this.key);

            return client.Execute<T>(request);
        }

        private List<OrdemServico> BuscaListaChamadosTecnicos()
        {
            List<OrdemServico> chamadosTransferidosCorreios = base.DbContext.OrdemServico.Where(w =>
            w.CodCliente == this.codClienteCorreios && w.CodStatusServico == 8 && w.NumOSCliente != null && w.CodContrato == 3469)
                .Include(inc => inc.Tecnico)
                .ToList();

            return chamadosTransferidosCorreios.ToList();
        }

        private string GetHtmlEmailTecnicoNaoCadastrado(string mensagem, TecnicoJson tecnico, string numOsCliente)
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
            texto.Append("							Dados do Técnico");
            texto.Append("						</font>");
            texto.Append("					</td>");
            texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Número Chamado:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", numOsCliente);
            texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Técnico:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.nome_tecnico);
            texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">CPF:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.cpf_tecnico);
            texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">RG:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.rg_tecnico);
            texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">E-mail:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.email_tecnico);
            texto.Append("				</tr>");

            //texto.Append("				<tr>");
            //texto.Append("					<td align=\"right\">Fone:</td>");
            //texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.FONE);
            //texto.Append("				</tr>");

            texto.Append("				<tr>");
            texto.Append("					<td align=\"right\">Fone Celular:</td>");
            texto.AppendFormat("					<td align=\"left\">{0}</td>", tecnico.tel_tecnico);
            texto.Append("				</tr>");
            texto.Append("			</table>");
            texto.Append("	</body>");
            texto.Append("</html>");

            return texto.ToString();
        }

        #endregion
    }
}

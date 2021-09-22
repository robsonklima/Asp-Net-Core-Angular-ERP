using SAT.INFRA.Context;
using SAT.INTEGRACOES.Interfaces;
using SAT.MODELS.Entities;
using System;
using System.Linq;
using SAT.SERVICES.Services;
using System.Collections.Generic;
using SAT.INTEGRACOES.Enums;
using SAT.INFRA.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SAT.INTEGRACOES
{
    public class BaseIntegracao : IBaseIntegracao
    {
        private AppDbContext _context;
        private SequenciaRepository _sequenciaRepository;
        private EmailService _emailService;
        private List<Action> _listEvents = new List<Action>();

        protected Integracao Integracao;
        protected AppDbContext DbContext { get { return this._context; } }
        protected SequenciaRepository SequenciaRepository { get { return this._sequenciaRepository; } }
        protected EmailService EmailService { get { return this._emailService; } }

        public string NomeIntegracao { get => ((IIntegracao)this).GetType().FullName; }
        public Action eventVerificaNovaIntegracao { get; set; }
        public Action eventTransformaDadosIntegracao { get; set; }
        public Action eventExecutaIntegracao { get; set; }
        public Action eventRetornoIntegracao { get; set; }

        protected T BuscaRequisicao<T>()
        {
            Integracao integracao = this._context.Integracao.Where(i => i == this.Integracao)
                .Include(arq => arq.IntegracaoArquivo)
                .FirstOrDefault();

            if (integracao == null)
            {
                LoggerService.LogError($"Integração não encontrada no DB: {NomeIntegracao} - {this.Integracao}");
                return default;
            }
            else
            {
                Type classe = Type.GetType(integracao.IntegracaoArquivo.Classe);
                return (T)JsonConvert.DeserializeObject(integracao.IntegracaoArquivo.TextoRequisicao, classe);
            }
        }

        protected List<Integracao> BuscaDadosIntegracaoEmFila()
        {
            return this._context.Integracao.Where(i => i.StatusIntegracao == StatusIntegracaoEnum.REGISTRADO.ToString() && i.NomeIntegracao == this.NomeIntegracao)
                .ToList();
        }

        protected List<Integracao> BuscaDadosIntegracaoPreparadosParaExecucao()
        {
            return this._context.Integracao.Where(i => i.StatusIntegracao == StatusIntegracaoEnum.EM_FILA_DE_EXECUCAO.ToString() && i.NomeIntegracao == this.NomeIntegracao)
                   .Include(i => i.IntegracaoArquivo)
                   .ToList();
        }

        protected List<Integracao> BuscaDadosIntegracaoEnviarRetorno()
        {
            return this._context.Integracao.
                Where(i =>
               (i.StatusIntegracao == StatusIntegracaoEnum.AGUARDANDO_ENVIO_RETORNO.ToString()
                || i.StatusIntegracao == StatusIntegracaoEnum.AGUARDANDO_ENVIO_AVISO_TECNICO_NAO_CADASTRADO.ToString())
                && i.NomeIntegracao == this.NomeIntegracao)
                   .ToList();
        }

        public BaseIntegracao()
        {
            this.eventVerificaNovaIntegracao = () => this.Start(() => ((IIntegracao)this).VerificaNovaIntegracao());
            this.eventTransformaDadosIntegracao = () => this.Start(() => ((IIntegracao)this).TransformaDadosIntegracao());
            this.eventExecutaIntegracao = () => this.Start(() => ((IIntegracao)this).ExecutaIntegracao());
            this.eventRetornoIntegracao = () => this.Start(() => ((IIntegracao)this).RetornoIntegracao());

            this._listEvents.Add(this.eventVerificaNovaIntegracao);
            this._listEvents.Add(this.eventTransformaDadosIntegracao);
            this._listEvents.Add(this.eventExecutaIntegracao);
            this._listEvents.Add(this.eventRetornoIntegracao);
        }

        public void InjectDbContext(AppDbContext appDbContext)
        {
            this._context = appDbContext;
            this._sequenciaRepository = new SequenciaRepository(this._context);
        }

        public void InjectEmailService(EmailService emailService)
        {
            this._emailService = emailService;
        }

        public void Initialize()
        {
            // Verifica todos os eventos inicializados
            if (this._listEvents.Any(integracaoEvent => integracaoEvent == null))
            {
                LoggerService.LogError($"Evento de integração não inicializado: {NomeIntegracao}");
                return;
            }

            // Chama os eventos
            foreach (Action integracaoEvent in this._listEvents)
            {
                integracaoEvent?.Invoke();
            }
        }

        public void AtualizaFilaIntegracoes(StatusIntegracaoEnum statusIntegracaoEnum, object contexto = null, string mensagemAviso = null)
        {
            switch (statusIntegracaoEnum)
            {
                case StatusIntegracaoEnum.REGISTRADO:
                    {
                        this.Integracao = new Integracao();
                        this.Integracao.DataCadastro = DateTime.Now;
                        this.Integracao.DataUltimaExecucao = DateTime.Now;
                        this.Integracao.NomeIntegracao = this.NomeIntegracao;
                        this.Integracao.MensagemAviso = mensagemAviso;

                        this._context.Integracao.Add(this.Integracao);
                        this._context.SaveChanges();

                        this.Integracao.IntegracaoArquivo = new IntegracaoArquivo();
                        this.Integracao.IntegracaoArquivo.CodIntegracao = this.Integracao.CodIntegracao;

                        this._context.IntegracaoArquivo.Add(this.Integracao.IntegracaoArquivo);
                    }
                    break;
                case StatusIntegracaoEnum.EM_FILA_DE_EXECUCAO:
                    {
                        this.Integracao.DataUltimaExecucao = DateTime.Now;

                        //IntegracaoArquivo __integracaoArquivo = new IntegracaoArquivo();
                        //__integracaoArquivo.CodIntegracao = this.Integracao.CodIntegracao;
                        //__integracaoArquivo.Arquivo = arquivo;
                        //this._context.IntegracaoArquivo.Add(__integracaoArquivo);
                    }
                    break;
                case StatusIntegracaoEnum.EM_EXECUCAO:
                    break;
                case StatusIntegracaoEnum.AGUARDANDO_ENVIO_AVISO_TECNICO_NAO_CADASTRADO:
                    {

                    }
                    break;
                case StatusIntegracaoEnum.ENVIANDO_RETORNO:
                    {

                    }
                    break;
                case StatusIntegracaoEnum.COMPLETO:
                    break;
                case StatusIntegracaoEnum.ERRO:
                    {
                        if (this.Integracao != null)
                        {
                            this.Integracao.MensagemAviso = mensagemAviso;
                        }
                    }
                    break;

            }

            if (contexto != null)
            {
                string json = JsonConvert.SerializeObject(contexto);
                Type classe = contexto.GetType();

                this.Integracao.IntegracaoArquivo.Arquivo = json;
                this.Integracao.IntegracaoArquivo.Classe = classe.Name;
            }

            this.Integracao.StatusIntegracao = statusIntegracaoEnum.ToString();
            this._context.SaveChanges();
        }

        protected void Start(Action integracaoExecute)
        {
            LoggerService.LogInfo($"{DateTime.Now} - (Start) Iniciando a execução da integração: { NomeIntegracao }");

            try
            {
                this.BeforeExecute();
                integracaoExecute?.Invoke();
                this.AfterExecute();
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"{DateTime.Now} - (Start) Erro na execução da integração: { NomeIntegracao } : Exception {ex.InnerException.Message}");
            }
        }

        public void BeforeExecute()
        {
            try
            {
                LoggerService.LogInfo($"{DateTime.Now} - (BeforeExecute) Iniciando a pré execução da integração: { NomeIntegracao }");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"{DateTime.Now} - (BeforeExecute) Erro na execução da integração: { NomeIntegracao } : Exception {ex.InnerException.Message}");
            }
        }

        public void AfterExecute()
        {
            try
            {
                LoggerService.LogInfo($"{DateTime.Now} - (AfterExecute) Iniciando a pós execução da integração: { NomeIntegracao }");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"{DateTime.Now} - (AfterExecute) Erro na execução da integração: { NomeIntegracao } : Exception {ex.InnerException.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        private readonly IAgendaTecnicoRepository _agendaRepo;
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IRelatorioAtendimentoRepository _ratRepo;
        private readonly IMediaAtendimentoTecnicoRepository _mediaTecnicoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ITecnicoRepository _tecnicoRepo;
        private readonly IOrdemServicoRepository _osRepo;

        public AgendaTecnicoService(
            IAgendaTecnicoRepository agendaRepo,
            IOrdemServicoRepository osRepo,
            IMediaAtendimentoTecnicoRepository mediaTecnicoRepo,
            IRelatorioAtendimentoRepository ratRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            IUsuarioRepository usuarioRepo,
            ITecnicoRepository tecnicoRepo
        )
        {
            _agendaRepo = agendaRepo;
            _osRepo = osRepo;
            _ratRepo = ratRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _mediaTecnicoRepo = mediaTecnicoRepo;
            _usuarioRepo = usuarioRepo;
            _tecnicoRepo = tecnicoRepo;
        }

        public List<ViewAgendaTecnicoRecurso> ObterViewPorParametros(AgendaTecnicoParameters parameters)
        {
            List<ViewAgendaTecnicoRecurso> recursos = new();   

            var agendas = _agendaRepo.ObterViewPorParametros(parameters);
            var usuarios = ObterUsuarios(parameters);

            for (int i = 0; i < agendas.Count(); i++)
            {
                agendas[i].Cor = ObterCor(agendas[i]);
                agendas[i].Titulo = ObterTitulo(agendas[i]);
                agendas[i].Editavel = ObterPermissaoEdicao(agendas[i]);
            }

            foreach (var usuario in usuarios)
            {
                var agendasDoUsuario = agendas.Where(a => a.CodTecnico == usuario.CodTecnico);

                for (int i = 0; i < agendasDoUsuario.Count(); i++)
                {
                    if (agendas[i].InicioAtendimento.HasValue)
                        agendas[i].Inicio = agendas[i].InicioAtendimento;

                    if (agendas[i].FimAtendimento.HasValue)
                        agendas[i].Fim = agendas[i].FimAtendimento;
                }

                var qtdChamadosAtendidos = agendasDoUsuario
                    .Where(a => a.CodStatusServico == (int)StatusServicoEnum.FECHADO && a.Tipo == AgendaTecnicoTipoEnum.OS)
                    .GroupBy(a => a.CodOS)
                    .Count();

                var qtdChamadosTransferidos = agendasDoUsuario
                    .Where(a => a.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO && a.Tipo == AgendaTecnicoTipoEnum.OS)
                    .GroupBy(a => a.CodOS)
                    .Count();

                if (usuario.CodTecnico != null) {
                    recursos.Add(new ViewAgendaTecnicoRecurso() {
                        Id = usuario.CodTecnico.Value,
                        Nome = usuario.NomeUsuario,
                        CodUsuario = usuario.CodUsuario,
                        FonePerto = usuario.Tecnico.FonePerto,
                        QtdChamadosAtendidos = qtdChamadosAtendidos,
                        QtdChamadosTransferidos = qtdChamadosTransferidos,
                        Eventos = agendasDoUsuario
                    });
                }
            }

            return recursos;
        }

        public List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            return _agendaRepo.ObterPorParametros(parameters);
        }

        public AgendaTecnico ObterPorCodigo(int codigo)
        {
            return _agendaRepo.ObterPorCodigo(codigo);
        }

        public AgendaTecnico Atualizar(AgendaTecnico agenda)
        {
            removerHistorico(agenda);
            _agendaRepo.Atualizar(agenda);
            return agenda;
        }

        public void Criar(AgendaTecnico agenda)
        {
            removerHistorico(agenda);

            var os = this._osRepo.ObterPorCodigo(agenda.CodOS.Value);
            var inicioPeriodo = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0));
            var fimPeriodo = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
            var tempoMedioAtendimento = ObterTempoMedioAtendimento(agenda.CodTecnico.Value, os.CodTipoIntervencao);

            var agendasDaOS = _agendaRepo.ObterPorOS(agenda.CodOS.Value);
            var ultimaAgenda = agendasDaOS
                .Where(a => a.IndAtivo == 1 && a.Tipo == AgendaTecnicoTipoEnum.OS)
                .OrderByDescending(a => a.Inicio)
                .FirstOrDefault();

            var inicio = DateTime.Now;

            if (ultimaAgenda != null) {
                if (ultimaAgenda.Fim > DateTime.Now) {
                    inicio = ultimaAgenda.Fim.Value;
                }
            }
            else if (agenda != null && agenda.Inicio.HasValue) {
                inicio = agenda.Inicio.Value;
            }

            if (this.estaNoIntervalo(inicio)) {
                inicio = this.FimIntervalo(inicio);
            }

            var fim = inicio.AddMinutes(tempoMedioAtendimento);
            if (this.estaNoIntervalo(fim))
            {
                inicio = fim;
                fim = inicio.AddMinutes(tempoMedioAtendimento);
            }

            if (inicio > this.FimExpediente() && agenda.Inicio.Value.Date == DateTime.Now.Date) {
                inicio = DateTime.Now.AddDays(1).Date.Add(new TimeSpan(8, 0, 0));
                fim = inicio.AddMinutes(tempoMedioAtendimento);
            }

            var ultimoAgendamento = os.Agendamentos?.LastOrDefault()?.DataAgendamento;

            if (ultimoAgendamento != null) {
                if (ultimoAgendamento > DateTime.Now) {
                    inicio = ultimoAgendamento.Value;
                    fim = ultimoAgendamento.Value.AddMinutes(tempoMedioAtendimento);
                }
            }

            agenda.Inicio = inicio;
            agenda.Fim = fim;
            _agendaRepo.Criar(agenda);
        }

        private string ObterCor(ViewAgendaTecnicoEvento agenda)
        {   
            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKIN)
                return Constants.COR_PRETO;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKOUT)
                return Constants.COR_PRETO;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.FIM_EXPEDIENTE)
                return Constants.COR_PRETO;

            if (agenda.CodStatusServico == (int)StatusServicoEnum.FECHADO)
                return Constants.COR_AZUL;

            if (agenda.Fim < DateTime.Now && agenda.CodStatusServico != (int)StatusServicoEnum.FECHADO && agenda.Tipo == AgendaTecnicoTipoEnum.OS)
                return Constants.COR_VERMELHO;
            
            if (agenda.DataAgendamento != null)
                return Constants.COR_ROXO;

            if (agenda.CodStatusServico == (int)StatusServicoEnum.PECA_FALTANTE)
                return Constants.COR_ROSA;

            if (agenda.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES)
                return Constants.COR_ROSA;

            if (agenda.CodStatusServico == (int)StatusServicoEnum.PECA_EM_TRANSITO)
                return Constants.COR_ROSA;

            if (agenda.CodStatusServico == (int)StatusServicoEnum.PECAS_LIBERADAS)
                return Constants.COR_TERRA;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.INTERVALO)
                return Constants.COR_CINZA;
            
            if (agenda.CodStatusServico == (int)StatusServicoEnum.PARCIAL)
                return Constants.COR_VERDE_CLARO;

            return Constants.COR_VERDE_ESCURO;
        }

        private string ObterTitulo(ViewAgendaTecnicoEvento agenda) {
            if (agenda.Tipo == AgendaTecnicoTipoEnum.FIM_EXPEDIENTE)
                return "FIM EXPEDIENTE";

            if (agenda.Tipo == AgendaTecnicoTipoEnum.INTERVALO)
                return "INTERVALO";

            if (agenda.Tipo == AgendaTecnicoTipoEnum.PONTO)
                return "PONTO";

            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKIN)
                return "CHECKIN";

            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKOUT)
                return "CHECKOUT";

            return agenda.NomeLocal;
        }
 
        private bool ObterPermissaoEdicao(ViewAgendaTecnicoEvento agenda) {
            if (agenda.Tipo == AgendaTecnicoTipoEnum.PONTO)
                return false;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.FIM_EXPEDIENTE)
                return false;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKIN)
                return false;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.CHECKOUT)
                return false;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.OS && agenda.CodStatusServico == (int)StatusServicoEnum.FECHADO)
                return false;

            if (agenda.Tipo == AgendaTecnicoTipoEnum.OS && agenda.DataAgendamento != null)
                return false; 

            return true;
        }
    
        private List<Usuario> ObterUsuarios(AgendaTecnicoParameters parameters) {
            return _usuarioRepo.ObterPorParametros(new UsuarioParameters() {
                CodFilial = parameters.CodFilial,
                CodPerfil = (int)PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
                IndAtivo = 1,
                PAS = parameters.PAS,
                IndFerias = parameters.IndFerias,
                CodTecnicos = parameters.CodTecnicos
            });
        }

        private int ObterTempoMedioAtendimento(int codTecnico, int codTipoIntervencao) {
            var tempos = _tecnicoRepo.ObterTempoAtendimento(codTecnico);
            var media = tempos.Where(t => t.CodTipoIntervencao == codTipoIntervencao).FirstOrDefault();
            
            if (media != null)
                return media.TempoEmMinutos;

            return 60;
        }

        private void removerHistorico(AgendaTecnico agenda) {
            var parametros = new AgendaTecnicoParameters() { CodOS = agenda.CodOS.Value };
            
            var agendas = _agendaRepo
                .ObterPorParametros(parametros)
                .Where(a => a.CodAgendaTecnico != agenda.CodAgendaTecnico);

            foreach (var a in agendas)
            {
                a.IndAtivo = 0;
                a.DataHoraManut = DateTime.Now;
                a.CodUsuarioManut = Constants.SISTEMA_NOME;
                _agendaRepo.Atualizar(a);
            }
        }

        private bool estaNoIntervalo(DateTime time) => time >= this.InicioIntervalo(time) && time <= this.FimIntervalo(time);
        private DateTime InicioExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(8, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(8, 00, 0));
        private DateTime FimExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(18, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(18, 00, 0));
        private DateTime InicioIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(12, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(12, 00, 0));
        private DateTime FimIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(13, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(13, 00, 0));
    }
}
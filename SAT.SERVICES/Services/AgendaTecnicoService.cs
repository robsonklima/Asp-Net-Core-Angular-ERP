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
        private readonly IEmailService _emailService;
        private readonly IOrdemServicoRepository _osRepo;

        public AgendaTecnicoService(
            IAgendaTecnicoRepository agendaRepo,
            IOrdemServicoRepository osRepo,
            IMediaAtendimentoTecnicoRepository mediaTecnicoRepo,
            IRelatorioAtendimentoRepository ratRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            IUsuarioRepository usuarioRepo,
            ITecnicoRepository tecnicoRepo,
            IEmailService emailService
        )
        {
            _agendaRepo = agendaRepo;
            _osRepo = osRepo;
            _ratRepo = ratRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _mediaTecnicoRepo = mediaTecnicoRepo;
            _usuarioRepo = usuarioRepo;
            _tecnicoRepo = tecnicoRepo;
            _emailService = emailService;
        }

        public List<ViewAgendaTecnicoRecurso> ObterViewPorParametros(AgendaTecnicoParameters parameters)
        {
            List<ViewAgendaTecnicoRecurso> recursos = new();   

            var agendas = _agendaRepo.ObterViewPorParametros(parameters);
            var usuarios = ObterUsuarios(parameters);

            for (int i = 0; i < agendas.Count(); i++)
            {
                agendas[i].Cor = agendas[i].Tipo == AgendaTecnicoTipoEnum.OS ? agendas[i].Cor : ObterCor(agendas[i]);
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
            try
            {
                removerHistorico(agenda);

                var os = _osRepo.ObterPorCodigo(agenda.CodOS.Value);
                var tempoMedioAtendimento = ObterTempoMedioAtendimento(agenda.CodTecnico.Value, os.CodTipoIntervencao);
                var historicoAgenda = _agendaRepo.ObterPorOS(agenda.CodOS.Value);

                var inicio = DateTime.Now;

                if (inicioFoiInformado(agenda)) 
                    inicio = agenda.Inicio.Value;

                if (!inicioFoiInformado(agenda)) {
                    var ultimaAgenda = historicoAgenda
                        .Where(a => a.IndAtivo == 1 && a.Tipo == AgendaTecnicoTipoEnum.OS)
                        .OrderByDescending(a => a.Inicio)
                        .FirstOrDefault();

                    if (ultimaAgenda != null) 
                        if (ultimaAgenda.Fim > DateTime.Now) 
                            inicio = ultimaAgenda.Fim.Value.AddHours(1);
                }
                
                if (estaNoIntervalo(inicio))
                    inicio = fimIntervalo(inicio).AddHours(1);

                var fim = inicio.AddMinutes(tempoMedioAtendimento);
                if (estaNoIntervalo(fim))
                {
                    inicio = fimIntervalo().AddHours(1);
                    fim = inicio.AddMinutes(tempoMedioAtendimento);
                }

                if (inicio > fimExpediente() && inicio.Date == DateTime.Now.Date) {
                    inicio = DateTime.Now.AddDays(1).Date.Add(new TimeSpan(8, 0, 0));
                    fim = inicio.AddMinutes(tempoMedioAtendimento);
                }

                var agendamento = os.Agendamentos?.LastOrDefault()?.DataAgendamento;
                if (agendamento != null) {
                    inicio = agendamento.Value;
                    fim = agendamento.Value.AddMinutes(tempoMedioAtendimento);
                }

                agenda.Inicio = inicio;
                agenda.Fim = fim;

                _agendaRepo.Criar(agenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar agenda do técnico para a OS {agenda.CodOS}", ex);
            }
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

            if (agenda.Fim < DateTime.Now && 
                agenda.CodStatusServico != (int)StatusServicoEnum.FECHADO && 
                agenda.CodStatusServico != (int)StatusServicoEnum.PECAS_PENDENTES &&
                agenda.CodStatusServico != (int)StatusServicoEnum.PECAS_LIBERADAS &&
                agenda.CodStatusServico != (int)StatusServicoEnum.PECA_EM_TRANSITO &&
                agenda.CodStatusServico != (int)StatusServicoEnum.PECA_SEPARADA &&
                agenda.Tipo == AgendaTecnicoTipoEnum.OS)
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
            var media = tempos
                .Where(t => t.CodTipoIntervencao == codTipoIntervencao)
                .FirstOrDefault();
            
            if (media != null)
                return media.TempoEmMinutos;

            return 60;
        }

        private void removerHistorico(AgendaTecnico agenda) {
            if (agenda?.CodOS == null) 
                return;

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

        private bool estaNoIntervalo(DateTime time) => time >= inicioIntervalo(time) && time <= fimIntervalo(time);
        private DateTime inicioExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(8, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(8, 00, 0));
        private DateTime fimExpediente(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(18, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(18, 00, 0));
        private DateTime inicioIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(12, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(12, 00, 0));
        private DateTime fimIntervalo(DateTime? referenceTime = null) => 
            referenceTime.HasValue ? referenceTime.Value.Date.Add(new TimeSpan(13, 00, 0)) : DateTime.Now.Date.Add(new TimeSpan(13, 00, 0));
        
        private bool inicioFoiInformado(AgendaTecnico agenda) => agenda.Inicio.HasValue;
    }
}
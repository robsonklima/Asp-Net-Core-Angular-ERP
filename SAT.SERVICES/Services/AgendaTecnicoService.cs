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
        private readonly IOrdemServicoRepository _osRepo;

        public AgendaTecnicoService(
            IAgendaTecnicoRepository agendaRepo,
            IOrdemServicoRepository osRepo,
            IMediaAtendimentoTecnicoRepository mediaTecnicoRepo,
            IRelatorioAtendimentoRepository ratRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            IUsuarioRepository usuarioRepo
        )
        {
            _agendaRepo = agendaRepo;
            _osRepo = osRepo;
            _ratRepo = ratRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _mediaTecnicoRepo = mediaTecnicoRepo;
            _usuarioRepo = usuarioRepo;
        }

        public List<ViewAgendaTecnicoRecurso> ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendas = _agendaRepo.ObterPorParametros(parameters);


            for (int i = 0; i < agendas.Count; i++)
            {
                agendas[i].Cor = ObterCor(agendas[i]);
                agendas[i].Titulo = ObterTitulo(agendas[i]);
                agendas[i].Editavel = ObterPermissaoEdicao(agendas[i]);

                if (agendas[i].CodAgendaTecnico == null && agendas[i].Tipo == AgendaTecnicoTipoEnum.INTERVALO)
                    agendas[i] = InserirIntervalo(agendas[i]);
            }

            var usuarios = _usuarioRepo.ObterPorParametros(new UsuarioParameters() {
                CodFilial = parameters.CodFilial,
                CodPerfil = (int)PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
                IndAtivo = 1
            });

            List<ViewAgendaTecnicoRecurso> recursos = new();

            foreach (var usuario in usuarios)
            {
                var agendasDoUsuario = agendas.Where(a => a.CodTecnico == usuario.CodTecnico);
                var qtdChamadosAtendidos = agendasDoUsuario.Where(a => a.CodStatusServico == (int)StatusServicoEnum.FECHADO).Count();
                var qtdChamadosTransferidos = agendasDoUsuario.Where(a => a.CodStatusServico != (int)StatusServicoEnum.FECHADO).Count();

                if (usuario.CodTecnico != null)
                    recursos.Add(new ViewAgendaTecnicoRecurso() {
                        Id = (int)usuario.CodTecnico,
                        Nome = usuario.NomeUsuario,
                        CodUsuario = usuario.CodUsuario,
                        FonePerto = usuario.Tecnico.FonePerto,
                        QtdChamadosAtendidos = qtdChamadosAtendidos,
                        QtdChamadosTransferidos = qtdChamadosTransferidos,
                        Eventos = agendasDoUsuario
                    });
            }

            return recursos;
        }

        public AgendaTecnico ObterPorCodigo(int codigo)
        {
            return _agendaRepo.ObterPorCodigo(codigo);
        }

        public AgendaTecnico Atualizar(AgendaTecnico agenda)
        {
            _agendaRepo.Atualizar(agenda);
            return agenda;
        }

        public void Criar(AgendaTecnico agenda)
        {
            _agendaRepo.Criar(agenda);
        }

        public void Deletar(int codAgendaTecnico)
        {
            this._agendaRepo.Deletar(codAgendaTecnico);
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
    
        private ViewAgendaTecnicoEvento InserirIntervalo(ViewAgendaTecnicoEvento agenda) {
            var agendaTecnico = _agendaRepo.Criar(new AgendaTecnico() {
                Inicio = agenda.Inicio,
                Fim = agenda.Fim,
                CodTecnico = agenda.CodTecnico,
                DataHoraCad = DateTime.Now,
                CodUsuarioCad = "SAT",
                IndAtivo = 1,
                Tipo = agenda.Tipo,
                Titulo = agenda.Titulo
            });

            agenda.IndAtivo = 1;
            agenda.DataHoraCad = agendaTecnico.DataHoraCad;
            agenda.CodAgendaTecnico = agendaTecnico.CodAgendaTecnico;

            return agenda;
        }
    }
}
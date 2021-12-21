using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        private readonly IAgendaTecnicoRepository _agendaRepo;
        private readonly ITecnicoRepository _tecnicoRepo;
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IOrdemServicoRepository _osRepo;

        public AgendaTecnicoService(
            IAgendaTecnicoRepository agendaRepo,
            ITecnicoRepository tecnicoRepo,
            IOrdemServicoRepository osRepo,
            IPontoUsuarioRepository pontoUsuarioRepo)
        {
            _agendaRepo = agendaRepo;
            _tecnicoRepo = tecnicoRepo;
            _osRepo = osRepo;
            _pontoUsuarioRepo = pontoUsuarioRepo;
        }

        public ListViewModel ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendamentos = _agendaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = agendamentos,
                TotalCount = agendamentos.TotalCount,
                CurrentPage = agendamentos.CurrentPage,
                PageSize = agendamentos.PageSize,
                TotalPages = agendamentos.TotalPages,
                HasNext = agendamentos.HasNext,
                HasPrevious = agendamentos.HasPrevious
            };

            return lista;
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

        public void Deletar(int codigo)
        {
            _agendaRepo.Deletar(codigo);
        }

        public void Criar(AgendaTecnico agenda)
        {
            if (agenda.CodOS.HasValue) this.DeletarAgendaTecnico(agenda.CodOS.Value);
            _agendaRepo.Criar(agenda);
        }
    }
}
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _agendamentoRepo = agendamentoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(Agendamento agendamento)
        {
            _agendamentoRepo.Atualizar(agendamento);
        }

        public Agendamento Criar(Agendamento agendamento)
        {
            agendamento.CodAgendamento = _sequenciaRepo.ObterContador("AgendamentoOS");
            _agendamentoRepo.Criar(agendamento);
            return agendamento;
        }

        public void Deletar(int codigo)
        {
            _agendamentoRepo.Deletar(codigo);
        }

        public Agendamento ObterPorCodigo(int codigo)
        {
            return _agendamentoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AgendamentoParameters parameters)
        {
            var agendamentos = _agendamentoRepo.ObterPorParametros(parameters);

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
    }
}

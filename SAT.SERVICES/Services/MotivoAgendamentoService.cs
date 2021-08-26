using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MotivoAgendamentoService : IMotivoAgendamentoService
    {
        private readonly IMotivoAgendamentoRepository _motivoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public MotivoAgendamentoService(IMotivoAgendamentoRepository motivoRepo, ISequenciaRepository seqRepo)
        {
            _motivoRepo = motivoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(MotivoAgendamentoParameters parameters)
        {
            var motivos = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = motivos,
                TotalCount = motivos.TotalCount,
                CurrentPage = motivos.CurrentPage,
                PageSize = motivos.PageSize,
                TotalPages = motivos.TotalPages,
                HasNext = motivos.HasNext,
                HasPrevious = motivos.HasPrevious
            };

            return lista;
        }

        public MotivoAgendamento Criar(MotivoAgendamento motivoAgendamento)
        {
            motivoAgendamento.CodMotivo = _seqRepo.ObterContador(Constants.TABELA_MOTIVO_AGENDAMENTO);
            _motivoRepo.Criar(motivoAgendamento);
            return motivoAgendamento;
        }

        public void Deletar(int codigo)
        {
            _motivoRepo.Deletar(codigo);
        }

        public void Atualizar(MotivoAgendamento motivoAgendamento)
        {
            _motivoRepo.Atualizar(motivoAgendamento);
        }

        public MotivoAgendamento ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}

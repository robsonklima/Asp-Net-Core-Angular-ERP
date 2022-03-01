using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class StatusServicoService : IStatusServicoService
    {
        private readonly IStatusServicoRepository _statusServicoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public StatusServicoService(IStatusServicoRepository statusServicoRepo, ISequenciaRepository seqRepo)
        {
            _statusServicoRepo = statusServicoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(StatusServicoParameters parameters)
        {
            var statusServicos = _statusServicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = statusServicos,
                TotalCount = statusServicos.TotalCount,
                CurrentPage = statusServicos.CurrentPage,
                PageSize = statusServicos.PageSize,
                TotalPages = statusServicos.TotalPages,
                HasNext = statusServicos.HasNext,
                HasPrevious = statusServicos.HasPrevious
            };

            return lista;
        }

        public StatusServico Criar(StatusServico statusServico)
        {
            statusServico.CodStatusServico = _seqRepo.ObterContador("StatusServico");
            _statusServicoRepo.Criar(statusServico);
            return statusServico;
        }

        public void Deletar(int codigo)
        {
            _statusServicoRepo.Deletar(codigo);
        }

        public void Atualizar(StatusServico statusServico)
        {
            _statusServicoRepo.Atualizar(statusServico);
        }

        public StatusServico ObterPorCodigo(int codigo)
        {
            return _statusServicoRepo.ObterPorCodigo(codigo);
        }
    }
}
